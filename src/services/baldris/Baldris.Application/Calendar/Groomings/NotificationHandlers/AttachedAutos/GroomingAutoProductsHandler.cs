using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.CreateByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.DeleteByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedProducts.Commands.DeleteByTriggeringId;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using Baldris.Entities.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Groomings.NotificationHandlers.AttachedAutos
{
    internal class GroomingAutoProductsHandler : INotificationHandler<EntityCreatedNotification<Grooming>>,
        INotificationHandler<EntityUpdatedNotification<Grooming>>,
        INotificationHandler<EntityDeletedNotification<Grooming>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public GroomingAutoProductsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
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
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);

                // CalendarEventType
                if (notification.CreatedValue.CalendarEventTypeId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<CalendarEventType>(
                        notification.CreatedValue.CalendarEventTypeId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                         departmentId, Discriminator.Grooming, null), cancellationToken);
                }
                
                // Location
                if (notification.CreatedValue.LocationId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.LocationId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Grooming, null), cancellationToken);
                }
                
                // Origin
                if (notification.CreatedValue.OriginId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.OriginId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Transport, null), cancellationToken);
                }
                
                // Destination
                if (notification.CreatedValue.DestinationId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.DestinationId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Transport, null), cancellationToken);
                }
                
                // Casket Equipment
                if (notification.CreatedValue.CasketEquipmentId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.CasketEquipmentId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Grooming, null), cancellationToken);
                }
                
                // Transport By
                if (notification.CreatedValue.TransportById.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.TransportById.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Transport, null), cancellationToken);
                }
                
                // Clothing
                if (notification.CreatedValue.ClothingId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.ClothingId.Value, notification.CreatedId,
                        nameof(CalendarEvent), notification.GoverningId.Value,
                        departmentId, Discriminator.Grooming, null), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Grooming> notification,
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
                            departmentId, Discriminator.Grooming, null), cancellationToken);
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
                            departmentId, Discriminator.Grooming, null), cancellationToken);
                    }
                }
                
                // Origin
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.OriginId != notification.CurrentValue.OriginId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.OriginId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.OriginId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.OriginId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.OriginId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, Discriminator.Transport, null), cancellationToken);
                    }
                }
                
                // Destination
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.DestinationId != notification.CurrentValue.DestinationId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.DestinationId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.DestinationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.DestinationId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.DestinationId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, Discriminator.Transport, null), cancellationToken);
                    }
                }
                
                // Casket Equipment
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.CasketEquipmentId != notification.CurrentValue.CasketEquipmentId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.CasketEquipmentId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.CasketEquipmentId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.CasketEquipmentId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.CasketEquipmentId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, Discriminator.Grooming, null), cancellationToken);
                    }
                }
                
                // Transport By
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.TransportById != notification.CurrentValue.TransportById)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.TransportById.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.TransportById.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.TransportById.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.TransportById.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, Discriminator.Transport, null), cancellationToken);
                    }
                }
                
                // Clothing
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.ClothingId != notification.CurrentValue.ClothingId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.ClothingId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.ClothingId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.ClothingId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.ClothingId.Value, notification.UpdatedId,
                            nameof(CalendarEvent), notification.GoverningId.Value,
                            departmentId, Discriminator.Grooming, null), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Grooming> notification,
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