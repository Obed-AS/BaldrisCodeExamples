using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEventTypes.Commands.Delete
{
    internal class DeleteCalendarEventTypeHandler : IRequestHandler<DeleteCalendarEventTypeCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteCalendarEventTypeHandler(IBaldrisDbContextFactory dbContextFactory,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(DeleteCalendarEventTypeCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var previousValue =
                await dbContext.CalendarEventTypes.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.CalendarEventTypeId, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(CalendarEventType), request.CalendarEventTypeId);
            }

            if (previousValue.IsInternal)
            {
                throw new ForbiddenOperationException(nameof(Report), request.CalendarEventTypeId, "Entity is marked as internal.");
            }

            if (previousValue.IsRequiredBySystem)
            {
                throw new ForbiddenOperationException(nameof(Report), request.CalendarEventTypeId, "Entity is marked as required by system.");
            }

            dbContext.CalendarEventTypes.Remove(previousValue);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityDeletedNotification<CalendarEventType>
                    { DeletedId = previousValue.Id, DeletedValue = previousValue, UserId = userId },
                cancellationToken);

            return result;
        }


    }
}
