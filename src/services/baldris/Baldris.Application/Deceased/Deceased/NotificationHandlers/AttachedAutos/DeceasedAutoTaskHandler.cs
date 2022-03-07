using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedAutoTasks.Commands.CreateByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.AttachedAutoTasks.Commands.DeleteByAttachedTypeId;
using Baldris.Application.AutoTasksAndAutoProducts.Models;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using Baldris.Entities.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.NotificationHandlers.AttachedAutos
{
    internal class DeceasedAutoTaskHandler : INotificationHandler<EntityCreatedNotification<Entities.Common.Deceased>>,
        INotificationHandler<EntityUpdatedNotification<Entities.Common.Deceased>>,
        INotificationHandler<EntityDeletedNotification<Entities.Common.Deceased>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public DeceasedAutoTaskHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task Handle(EntityCreatedNotification<Entities.Common.Deceased> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                // Get possible values from db
                var values = await GetReplacementValues(notification.CreatedId, notification.GoverningId.Value,
                    cancellationToken);

                // Casket
                if (notification.CreatedValue.CasketId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Casket>(
                        notification.CreatedValue.CasketId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                         values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Casket,
                            LocationName = values.PlaceOfDeath
                        }), cancellationToken);
                }

                // Urn
                if (notification.CreatedValue.UrnId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Urn>(
                        notification.CreatedValue.UrnId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                        values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.Urn,
                            LocationName = values.Cemetery
                        }), cancellationToken);
                }
                
                // Place of Death
                if (notification.CreatedValue.PlaceOfDeathId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.PlaceOfDeathId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                        values.DepartmentId, Discriminator.PlaceOfDeath, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.PlaceOfDeath,
                            LocationName = values.PlaceOfDeath
                        }), cancellationToken);
                }
                
                // Municipality of Birth
                if (notification.CreatedValue.MunicipalityOfBirthId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfBirthId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                        values.DepartmentId, Discriminator.MunicipalityOfBirth, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.MunicipalityOfBirth,
                            LocationName = values.MunicipalityOfBirth
                        }), cancellationToken);
                }
                
                // Municipality of Residence
                if (notification.CreatedValue.MunicipalityOfResidenceId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfResidenceId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                        values.DepartmentId, Discriminator.MunicipalityOfResidence, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.MunicipalityOfResidence,
                            LocationName = values.MunicipalityOfResidence
                        }), cancellationToken);
                }
                
                // Municipality of Death
                if (notification.CreatedValue.MunicipalityOfDeathId.HasValue)
                {
                    await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfDeathId.Value, notification.CreatedValue.Id,
                        nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                        values.DepartmentId, Discriminator.MunicipalityOfDeath, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                        {
                            DeceasedNames = values.Deceased,
                            Value = values.MunicipalityOfDeath,
                            LocationName = values.MunicipalityOfDeath
                        }), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Entities.Common.Deceased> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                // Get possible values from db
                var values = await GetReplacementValues(notification.UpdatedId, notification.GoverningId.Value,
                    cancellationToken);
                
                // Casket
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.CasketId != notification.CurrentValue.CasketId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.CasketId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Casket>(
                            notification.PreviousValue.CasketId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.CasketId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Casket>(
                            notification.CurrentValue.CasketId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Casket,
                                LocationName = values.PlaceOfDeath
                            }), cancellationToken);
                    }
                }
                // TODO: Handle CasketType?

                // Urn
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.UrnId != notification.CurrentValue.UrnId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.UrnId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Urn>(
                            notification.PreviousValue.UrnId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.UrnId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Urn>(
                            notification.CurrentValue.UrnId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.Urn,
                                LocationName = values.PlaceOfDeath
                            }), cancellationToken);
                    }
                }
                // TODO: Handle UrnType?

                // Place of Death
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.PlaceOfDeathId != notification.CurrentValue.PlaceOfDeathId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.PlaceOfDeathId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.PlaceOfDeathId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.PlaceOfDeathId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.PlaceOfDeathId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.PlaceOfDeath,
                                LocationName = values.PlaceOfDeath
                            }), cancellationToken);
                    }
                }
                // TODO: Handle LocationTypes?

                // Municipality of Birth
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.MunicipalityOfBirthId != notification.CurrentValue.MunicipalityOfBirthId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.MunicipalityOfBirthId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfBirthId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.MunicipalityOfBirthId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfBirthId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.MunicipalityOfBirth,
                                LocationName = values.MunicipalityOfBirth
                            }), cancellationToken);
                    }
                }

                // Municipality of Residence
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.MunicipalityOfResidenceId != notification.CurrentValue.MunicipalityOfResidenceId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.MunicipalityOfResidenceId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfResidenceId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.MunicipalityOfResidenceId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfResidenceId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.MunicipalityOfResidence,
                                LocationName = values.MunicipalityOfResidence
                            }), cancellationToken);
                    }
                }

                // Municipality of Death
                // Only update AutoTasks if value has changed
                if (notification.PreviousValue.MunicipalityOfDeathId != notification.CurrentValue.MunicipalityOfDeathId)
                {
                    // Remove previous AutoTasks
                    if (notification.PreviousValue.MunicipalityOfDeathId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfDeathId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value));
                    }
                    
                    // Add new AutoTasks
                    if (notification.CurrentValue.MunicipalityOfDeathId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoTasksByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfDeathId.Value, notification.UpdatedId,
                            nameof(Entities.Common.Deceased), notification.GoverningId.Value, 
                            values.DepartmentId, null, NoteOrTaskTab.Deceased, null, new AutoTaskReplacements
                            {
                                DeceasedNames = values.Deceased,
                                Value = values.MunicipalityOfDeath,
                                LocationName = values.MunicipalityOfDeath
                            }), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Entities.Common.Deceased> notification,
            CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
        }

        private async Task<dynamic> GetReplacementValues(Guid deceasedId, Guid workOrderId, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            return await dbContext.WorkOrderDeceased
                .Where(x => x.WorkOrderId == workOrderId && x.DeceasedId == deceasedId)
                .Select(x => new
                {
                    Deceased = x.Deceased.Person.DisplayName,
                    Casket = x.Deceased.Casket.Name,
                    // CasketTypeId = x.Deceased.Casket.CasketTypeId,
                    Urn = x.Deceased.Urn.Name,
                    // UrnTypeId = x.Deceased.Urn.UrnTypeId,
                    PlaceOfDeath = x.Deceased.PlaceOfDeath.Name,
                    // PlaceOfDeathTypeIds = x.Deceased.PlaceOfDeath.LocationTypes.Select(y => y.LocationTypeId),
                    MunicipalityOfBirth = x.Deceased.MunicipalityOfBirth.Name,
                    MunicipalityOfResidence = x.Deceased.MunicipalityOfResidence.Name,
                    MunicipalityOfDeath = x.Deceased.MunicipalityOfDeath.Name,
                    Cemetery = x.Deceased.Grave.Cemetery.Name,
                    DepartmentId = x.WorkOrder.DepartmentId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}