using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.ParticipantRoles.Commands.Delete
{
    internal class DeleteParticipantRoleHandler : IRequestHandler<DeleteParticipantRoleCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteParticipantRoleHandler(IBaldrisDbContextFactory dbContextFactory,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(DeleteParticipantRoleCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var previousValue =
                await dbContext.ParticipantRoles.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.ParticipantRoleId, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(ParticipantRole), request.ParticipantRoleId);
            }

            dbContext.ParticipantRoles.Remove(previousValue);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityDeletedNotification<ParticipantRole>
                    { DeletedId = previousValue.Id, DeletedValue = previousValue, UserId = userId },
                cancellationToken);

            return result;
        }


    }
}
