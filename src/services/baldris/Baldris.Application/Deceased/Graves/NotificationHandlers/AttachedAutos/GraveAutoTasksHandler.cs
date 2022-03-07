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

namespace Baldris.Application.Deceased.Graves.NotificationHandlers.AttachedAutos
{
    internal class GraveAutoTasksHandler : INotificationHandler<EntityCreatedNotification<Grave>>,
        INotificationHandler<EntityUpdatedNotification<Grave>>,
        INotificationHandler<EntityDeletedNotification<Grave>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public GraveAutoTasksHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task Handle(EntityCreatedNotification<Grave> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                // Get possible values from db
                var values = await GetReplacementValues(notification.CreatedId, notification.GoverningId.Value,
                    cancellationToken);

                // GraveType
                if (notification.CreatedValue.GraveTypeId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<GraveType>(
                        notification.CreatedValue.GraveTypeId.Value, notification.CreatedValue.Id,
                        nameof(Grave), notification.GoverningId.Value,
                         values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Cemetery
                if (notification.CreatedValue.CemeteryId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.CemeteryId.Value, notification.CreatedValue.Id,
                        nameof(Grave), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
                
                // Temporary Marker
                if (notification.CreatedValue.TemporaryMarkerId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                        notification.CreatedValue.TemporaryMarkerId.Value, notification.CreatedValue.Id,
                        nameof(Grave), notification.GoverningId.Value,
                        values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Value,
                            LocationName = values.Location
                        }), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Grave> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                // Get possible values from db
                var values = await GetReplacementValues(notification.UpdatedId, notification.GoverningId.Value,
                    cancellationToken);

                // GraveType
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.GraveTypeId != notification.CurrentValue.GraveTypeId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.GraveTypeId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<GraveType>(
                            notification.PreviousValue.GraveTypeId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.GraveTypeId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<GraveType>(
                            notification.CurrentValue.GraveTypeId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // GraveType
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.CemeteryId != notification.CurrentValue.CemeteryId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.CemeteryId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.CemeteryId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.CemeteryId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.CemeteryId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
                
                // Temporary Marker
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.TemporaryMarkerId != notification.CurrentValue.TemporaryMarkerId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.TemporaryMarkerId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.PreviousValue.TemporaryMarkerId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoTasks
                    if (notification.CurrentValue.TemporaryMarkerId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<LookupString>(
                            notification.CurrentValue.TemporaryMarkerId.Value, notification.UpdatedId,
                            nameof(Grave), notification.GoverningId.Value,
                            values.DepartmentId, Discriminator.Grave, NoteOrTaskTab.Grave, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Value,
                                LocationName = values.Location
                            }), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Grave> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                await _mediator.Send(new DeleteAutoTasksByTriggeringIdCommand(notification.DeletedId, nameof(Grave),
                    notification.GoverningId.Value));
            }
        }

        private async Task<dynamic> GetReplacementValues(Guid entityId, Guid workOrderId, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var graveValues = await dbContext.Graves
                .Where(x => x.Id == entityId)
                .Select(x => new
                {
                    Deceased = x.Deceased.Select(y => y.Person.DisplayName),
                    Location = x.Cemetery.Name,
                    Value = x.GraveType.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
            var departmentId = await dbContext.WorkOrders.Where(x => x.Id == workOrderId).Select(x => x.DepartmentId)
                .FirstOrDefaultAsync(cancellationToken);
            
            return new
            {
                Deceased = string.Join(" // ", graveValues.Deceased),
                Location = graveValues.Location,
                Value = graveValues.Value,
                DepartmentId = departmentId
            };
        }
    }
}