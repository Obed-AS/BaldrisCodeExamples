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
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.NotificationHandlers.AttachedAutos
{
    internal class DeceasedAutoProductsHandler : INotificationHandler<EntityCreatedNotification<Entities.Common.Deceased>>,
        INotificationHandler<EntityUpdatedNotification<Entities.Common.Deceased>>,
        INotificationHandler<EntityDeletedNotification<Entities.Common.Deceased>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public DeceasedAutoProductsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMediator mediator)
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
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);

                // Casket
                if (notification.CreatedValue.CasketId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Casket>(
                        notification.CreatedValue.CasketId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                         departmentId, null, null), cancellationToken);
                }
                
                // Urn
                if (notification.CreatedValue.UrnId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Urn>(
                        notification.CreatedValue.UrnId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                        departmentId, null, null), cancellationToken);
                }
                
                // Place Of Death
                if (notification.CreatedValue.PlaceOfDeathId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                        notification.CreatedValue.PlaceOfDeathId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                        departmentId, Discriminator.PlaceOfDeath, null), cancellationToken);
                }
                
                // Municipality of Birth
                if (notification.CreatedValue.MunicipalityOfBirthId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfBirthId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                        departmentId, Discriminator.MunicipalityOfBirth, null), cancellationToken);
                }
                
                // Municipality of Residence
                if (notification.CreatedValue.MunicipalityOfResidenceId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfResidenceId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                        departmentId, Discriminator.MunicipalityOfResidence, null), cancellationToken);
                }
                
                // Municipality of Death
                if (notification.CreatedValue.MunicipalityOfResidenceId.HasValue)
                {
                    await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                        notification.CreatedValue.MunicipalityOfResidenceId.Value, notification.CreatedId,
                        nameof(Deceased), notification.GoverningId.Value,
                        departmentId, Discriminator.MunicipalityOfDeath, null), cancellationToken);
                }
            }
        }

        public async Task Handle(EntityUpdatedNotification<Entities.Common.Deceased> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
                var departmentId = await dbContext.WorkOrders.Where(x => x.Id == notification.GoverningId.Value).Select(x => x.DepartmentId)
                    .FirstOrDefaultAsync(cancellationToken);

                // Casket
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.CasketId != notification.CurrentValue.CasketId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.CasketId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Casket>(
                            notification.PreviousValue.CasketId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.CasketId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Casket>(
                            notification.CurrentValue.CasketId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, null, null), cancellationToken);
                    }
                }
                
                // Urn
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.UrnId != notification.CurrentValue.UrnId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.UrnId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Urn>(
                            notification.PreviousValue.UrnId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.UrnId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Urn>(
                            notification.CurrentValue.UrnId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, null, null), cancellationToken);
                    }
                }
                
                // Place of Death
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.PlaceOfDeathId != notification.CurrentValue.PlaceOfDeathId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.PlaceOfDeathId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.PreviousValue.PlaceOfDeathId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.PlaceOfDeathId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Location>(
                            notification.CurrentValue.PlaceOfDeathId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, Discriminator.PlaceOfDeath, null), cancellationToken);
                    }
                }
                
                // Municipality of Birth
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.MunicipalityOfBirthId != notification.CurrentValue.MunicipalityOfBirthId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.MunicipalityOfBirthId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfBirthId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.MunicipalityOfBirthId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfBirthId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, Discriminator.MunicipalityOfBirth, null), cancellationToken);
                    }
                }
                
                // Municipality of Residence
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.MunicipalityOfResidenceId != notification.CurrentValue.MunicipalityOfResidenceId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.MunicipalityOfResidenceId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfResidenceId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.MunicipalityOfResidenceId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfResidenceId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, Discriminator.MunicipalityOfResidence, null), cancellationToken);
                    }
                }
                
                // Municipality of Death
                // Only update AutoProducts if value has changed
                if (notification.PreviousValue.MunicipalityOfDeathId != notification.CurrentValue.MunicipalityOfDeathId)
                {
                    // Remove previous AutoProducts
                    if (notification.PreviousValue.MunicipalityOfDeathId.HasValue)
                    {
                        await _mediator.Send(new DeleteAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.PreviousValue.MunicipalityOfDeathId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value), cancellationToken);
                    }

                    // Add new AutoProducts
                    if (notification.CurrentValue.MunicipalityOfDeathId.HasValue)
                    {
                        await _mediator.Send(new CreateAutoProductsByAttachedTypeIdCommand<Municipality>(
                            notification.CurrentValue.MunicipalityOfDeathId.Value, notification.UpdatedId,
                            nameof(Deceased), notification.GoverningId.Value,
                            departmentId, Discriminator.MunicipalityOfDeath, null), cancellationToken);
                    }
                }
            }
        }

        public async Task Handle(EntityDeletedNotification<Entities.Common.Deceased> notification,
            CancellationToken cancellationToken)
        {
            if (notification.GoverningId.HasValue)
            {
                await _mediator.Send(new DeleteAutoProductsByTriggeringIdCommand(notification.DeletedId, nameof(Deceased),
                    notification.GoverningId.Value), cancellationToken);
            }
        }
    }
}