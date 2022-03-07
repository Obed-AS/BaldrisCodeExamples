using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedAutoTasks.Commands.CreateByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedAutoTasks.Commands.DeleteByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedAutoTasks.Commands.DeleteByTriggeringId;
using Baldris.Application.AutoTasksAndAutoProducts.Models;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using Baldris.Entities.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEvents.NotificationHandlers.AttachedAutos
{
    internal class CalendarEventAutoTaskHandler : INotificationHandler<EntityCreatedNotification<CalendarEvent>>,
        INotificationHandler<EntityUpdatedNotification<CalendarEvent>>,
        INotificationHandler<EntityDeletedNotification<CalendarEvent>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public CalendarEventAutoTaskHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
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
                // Get possible values from db
                var values = await GetReplacementValues(notification.CreatedId, notification.GoverningId.Value,
                    cancellationToken);

                // CalendarEventType
                if (notification.CreatedValue.CalendarEventTypeId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<CalendarEventType>(
                        notification.CreatedValue.CalendarEventTypeId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                         values.DepartmentId, null, GetNoteOrTaskTab(notification.CreatedValue), null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Location
                if (notification.CreatedValue.LocationId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.LocationId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, null, GetNoteOrTaskTab(notification.CreatedValue), null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<CalendarEvent> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                // Get possible values from db
                var values = await GetReplacementValues(notification.UpdatedId, notification.GoverningId.Value,
                    cancellationToken);

                // CalendarEventType
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.CalendarEventTypeId != notification.CurrentValue.CalendarEventTypeId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.CalendarEventTypeId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<CalendarEventType>(
                            notification.PreviousValue.CalendarEventTypeId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value));
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.CalendarEventTypeId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<CalendarEventType>(
                            notification.CurrentValue.CalendarEventTypeId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, null, GetNoteOrTaskTab(notification.CurrentValue), null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Location
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.LocationId != notification.CurrentValue.LocationId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.LocationId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.LocationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value));
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.LocationId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.LocationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, null, GetNoteOrTaskTab(notification.CurrentValue), null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<CalendarEvent> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                await _mediator.Send(new DeleteAutoTasksByTriggeringIdCommand(notification.DeletedId, nameof(CalendarEvent),
                    notification.GoverningId.Value));
            }
        }

        private async Task<dynamic> GetReplacementValues(Guid entityId, Guid workOrderId, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var values = await dbContext.CalendarEvents
                .Where(x => x.Id == entityId)
                .Select(x => new
                {
                    Deceased = x.Deceased.Select(y => y.Deceased.Person.DisplayName),
                    Location = x.LocationSerialized,
                    Value = x.Subject,
                    DepartmentId = x.WorkOrderId.HasValue ? x.WorkOrder.DepartmentId : null as Guid?
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new
            {
                Deceased = string.Join(" // ", values.Deceased),
                Location = values.Location,
                Value = values.Value,
                DepartmentId = values.DepartmentId
            };
        }

        private string GetNoteOrTaskTab(CalendarEvent calendarEvent)
        {
            if (calendarEvent is Grooming or Transport)
            {
                return NoteOrTaskTab.Transport;
            }
            if (calendarEvent is Ceremony)
            {
                return NoteOrTaskTab.Ceremony;
            }

            return nameof(calendarEvent);
        }
    }
}