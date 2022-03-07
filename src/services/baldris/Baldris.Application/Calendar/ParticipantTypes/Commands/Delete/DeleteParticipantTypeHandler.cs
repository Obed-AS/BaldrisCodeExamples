using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.ParticipantTypes.Commands.Delete
{
    internal class DeleteParticipantTypeHandler : IRequestHandler<DeleteParticipantTypeCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteParticipantTypeHandler(IBaldrisDbContextFactory dbContextFactory,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(DeleteParticipantTypeCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var previousValue =
                await dbContext.ParticipantTypes.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.ParticipantTypeId, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(ParticipantType), request.ParticipantTypeId);
            }

            if (previousValue.IsInternal)
            {
                throw new ForbiddenOperationException(nameof(Report), request.ParticipantTypeId, "Entity is marked as internal.");
            }

            if (previousValue.IsRequiredBySystem)
            {
                throw new ForbiddenOperationException(nameof(Report), request.ParticipantTypeId, "Entity is marked as required by system.");
            }

            dbContext.ParticipantTypes.Remove(previousValue);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityDeletedNotification<ParticipantType>
                    { DeletedId = previousValue.Id, DeletedValue = previousValue, UserId = userId },
                cancellationToken);

            return result;
        }


    }
}
