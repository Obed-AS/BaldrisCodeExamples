using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.CreateByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.DeleteByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.DeleteByTriggeringId;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEvents.NotificationHandlers.AttachedAutos
{
    internal class CalendarEventAutoProductsHandler : INotificationHandler<EntityCreatedNotification<CalendarEvent>>,
        INotificationHandler<EntityUpdatedNotification<CalendarEvent>>,
        INotificationHandler<EntityDeletedNotification<CalendarEvent>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public CalendarEventAutoProductsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task Handle(EntityCreatedNotification<CalendarEvent> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);

                // CalendarEventType
                if (notification.CreatedValue.CalendarEventTypeId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<CalendarEventType>(
                        notification.CreatedValue.CalendarEventTypeId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                         departmentId, null, null), cancellationToken);
                }
                
                // Location
                if (notification.CreatedValue.LocationId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.LocationId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, null, null), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<CalendarEvent> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);

                // CalendarEventType
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.CalendarEventTypeId != notification.CurrentValue.CalendarEventTypeId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.CalendarEventTypeId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<CalendarEventType>(
                            notification.PreviousValue.CalendarEventTypeId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.CalendarEventTypeId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<CalendarEventType>(
                            notification.CurrentValue.CalendarEventTypeId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, null, null), cancellationToken);
                    }
                }
                
                // Location
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.LocationId != notification.CurrentValue.LocationId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.LocationId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.LocationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.LocationId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.LocationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, null, null), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<CalendarEvent> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                await _mediator.Send(new DeleteAutoProductsByTriggeringIdCommand(notification.DeletedId, nameof(CalendarEvent),
                    notification.GoverningId.Value), cancellationToken);
            }
        }
    }
}