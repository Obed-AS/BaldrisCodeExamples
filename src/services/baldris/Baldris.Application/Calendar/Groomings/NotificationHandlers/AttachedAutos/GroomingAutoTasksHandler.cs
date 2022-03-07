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

namespace Baldris.Application.Calendar.Groomings.NotificationHandlers.AttachedAutos
{
    internal class GroomingAutoTasksHandler : INotificationHandler<EntityCreatedNotification<Grooming>>,
        INotificationHandler<EntityUpdatedNotification<Grooming>>,
        INotificationHandler<EntityDeletedNotification<Grooming>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public GroomingAutoTasksHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task Handle(EntityCreatedNotification<Grooming> notification,
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
                         values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
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
                        values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Origin
                if (notification.CreatedValue.OriginId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.OriginId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Destination
                if (notification.CreatedValue.DestinationId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.DestinationId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Casket Equipment
                if (notification.CreatedValue.CasketEquipmentId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.CasketEquipmentId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Transport by
                if (notification.CreatedValue.TransportById.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.TransportById.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Clothing
                if (notification.CreatedValue.ClothingId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.ClothingId.Value, notification.CreatedValue.Id,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Grooming> notification,
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
                            values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
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
                            values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Origin
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.OriginId != notification.CurrentValue.OriginId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.OriginId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.OriginId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.OriginId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.OriginId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Destination
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.DestinationId != notification.CurrentValue.DestinationId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.DestinationId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.DestinationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.DestinationId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.DestinationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Casket Equipment
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.CasketEquipmentId != notification.CurrentValue.CasketEquipmentId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.CasketEquipmentId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.CasketEquipmentId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.CasketEquipmentId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.CasketEquipmentId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Transport By
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.TransportById != notification.CurrentValue.TransportById)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.TransportById.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.TransportById.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.TransportById.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.TransportById.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Transport, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Clothing
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.ClothingId != notification.CurrentValue.ClothingId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.ClothingId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.ClothingId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.ClothingId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.ClothingId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Grooming, NoteOrTaskTab.Transport, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Grooming> notification,
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
    }
}