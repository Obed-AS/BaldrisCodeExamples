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

namespace Baldris.Application.Deceased.Graves.NotificationHandlers.AttachedAutos
{
    public class GraveAutoProductsHandler : INotificationHandler<EntityCreatedNotification<Grave>>,
        INotificationHandler<EntityUpdatedNotification<Grave>>,
        INotificationHandler<EntityDeletedNotification<Grave>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        
        public GraveAutoProductsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
        }
        
        public async Task Handle(EntityCreatedNotification<Grave> notification, CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);
                
                // GraveType
                if (notification.CreatedValue.GraveTypeId.HasValue)
                {
                    await _mediator.Send(
                        new CreateAutoProductsByAttachedTypeIdCommand<GraveType>(
                            notification.CreatedValue.GraveTypeId.Value, notification.CreatedId, nameof(Grave),
                            notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                        cancellationToken);
                }
                
                // Cemetery
                if (notification.CreatedValue.CemeteryId.HasValue)
                {
                    await _mediator.Send(
                        new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.CreatedValue.CemeteryId.Value, notification.CreatedId, nameof(Grave),
                            notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                        cancellationToken);
                }
                
                // Temporary Marker
                if (notification.CreatedValue.TemporaryMarkerId.HasValue)
                {
                    await _mediator.Send(
                        new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.CreatedValue.TemporaryMarkerId.Value, notification.CreatedId, nameof(Grave),
                            notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                        cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Grave> notification, CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);
                
                // GraveType
                // Only update AutoProducts if value has changed
                if (notification.CurrentValue.GraveTypeId != notification.PreviousValue.GraveTypeId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.GraveTypeId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<GraveType>(
                            notification.PreviousValue.GraveTypeId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }
                    
                    // Add new AutoProducts
                    if (notification.CurrentValue.GraveTypeId.HasValue)
                    {
                        await _mediator.Send(
                            new CreateAutoProductsByAttachedTypeIdCommand<GraveType>(
                                notification.CurrentValue.GraveTypeId.Value, notification.UpdatedId, nameof(Grave),
                                notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                            cancellationToken);
                    }
                }
                
                // Cemetery
                // Only update AutoProducts if value has changed
                if (notification.CurrentValue.CemeteryId != notification.PreviousValue.CemeteryId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.CemeteryId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.CemeteryId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }
                    
                    // Add new AutoProducts
                    if (notification.CurrentValue.CemeteryId.HasValue)
                    {
                        await _mediator.Send(
                            new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                                notification.CurrentValue.CemeteryId.Value, notification.UpdatedId, nameof(Grave),
                                notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                            cancellationToken);
                    }
                }
                
                // Temporary Marker
                // Only update AutoProducts if value has changed
                if (notification.CurrentValue.TemporaryMarkerId != notification.PreviousValue.TemporaryMarkerId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.TemporaryMarkerId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.TemporaryMarkerId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }
                    
                    // Add new AutoProducts
                    if (notification.CurrentValue.TemporaryMarkerId.HasValue)
                    {
                        await _mediator.Send(
                            new CreateAutoProductsByAttachedTypeIdCommand<LookupString>(
                                notification.CurrentValue.TemporaryMarkerId.Value, notification.UpdatedId, nameof(Grave),
                                notification.GoverningId.Value, departmentId, Discriminator.Grave, null),
                            cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Grave> notification, CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                await _mediator.Send(new DeleteAutoProductsByTriggeringIdCommand(notification.DeletedId, nameof(Grave),
                    notification.GoverningId.Value), cancellationToken);
            }
        }
    }
}