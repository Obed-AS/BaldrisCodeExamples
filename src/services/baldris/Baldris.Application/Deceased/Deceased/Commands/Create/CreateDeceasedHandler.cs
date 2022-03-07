using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Parties.Persons.Commands.UpdateOrInsertPerson;
using Baldris.Application.WorkOrders.WorkOrderDeceased.Commands.Create;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.Commands.Create
{
    internal class CreateDeceasedHandler : IRequestHandler<CreateDeceasedCommand, DeceasedDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateDeceasedHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<DeceasedDto> Handle(CreateDeceasedCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var deceased = _mapper.Map<CreateDeceasedCommand, Entities.Common.Deceased>(request);

            var createPersonCommand = new UpdateOrInsertPersonCommand
            {
                Id = request.PersonId == Guid.Empty ? null : request.PersonId,
                Sex = request.Sex,
                FirstName = request.FirstName,
                IsDeleted = false,
                LastName = request.LastName,
                UpdateOrInsertPrimaryAddressCommand = request.UpdateOrInsertAddressCommand,
                PrivacyOptions = PrivacyOptions.IsDeceasedEntity,
                DateOfBirth = request.DateOfBirth,
                DateOfDeath = request.DateOfDeath,
                SocialSecurityNumber = request.SocialSecurityNumber,
                DateOfBirthApproximate = request.DateOfBirthApproximate,
                DateOfDeathApproximate = request.DateOfDeathApproximate,
                ImageId = request.ImageId
            };

            var person = await _mediator.Send(createPersonCommand, cancellationToken);
            deceased.PersonId = person.Id;
            
            // Extract booleans from options
            deceased.IsToBeCremated = deceased.Options.HasFlag(DeceasedOptions.IsToBeCremated);
            deceased.IsAuthoritiesNoticedOfCremation = deceased.Options.HasFlag(DeceasedOptions.IsAuthoritiesNoticedOfCremation);
            deceased.IsApplyingForAshSpreading = deceased.Options.HasFlag(DeceasedOptions.IsApplyingForAshSpreading);
            deceased.IsFamilyAttendingUrnLowering = deceased.Options.HasFlag(DeceasedOptions.IsFamilyAttendingUrnLowering);
            deceased.IsTheRemainsToBeViewed = deceased.Options.HasFlag(DeceasedOptions.IsTheRemainsToBeViewed);
            deceased.IsOutOfTown = deceased.Options.HasFlag(DeceasedOptions.IsOutOfTown);
            deceased.WasStillborn = deceased.Options.HasFlag(DeceasedOptions.WasStillborn);
            deceased.IsGraveRegistrar = deceased.Options.HasFlag(DeceasedOptions.IsGraveRegistrar);
            deceased.IsGovernmentEmployee = deceased.Options.HasFlag(DeceasedOptions.IsGovernmentEmployee);
            deceased.IsToBeShipped = deceased.Options.HasFlag(DeceasedOptions.IsToBeShipped);

            await dbContext.Deceased.AddAsync(deceased, cancellationToken);
            
            var userId = await _loggedInUserService.GetUserIdAsync();
            await dbContext.SaveChangesAsync(userId, cancellationToken);

            if (request.TargetWorkOrderId.HasValue)
            {
                var existingDeceasedCount =
                    await dbContext.WorkOrderDeceased.CountAsync(x => x.WorkOrderId == request.TargetWorkOrderId,
                        cancellationToken);
                // Add WorkOrderDeceased
                await _mediator.Send(new CreateWorkOrderDeceasedCommand(deceased.Id, request.TargetWorkOrderId.Value,
                    existingDeceasedCount > 0 ? await dbContext.WorkOrderDeceased.Where(x => x.WorkOrderId == request.TargetWorkOrderId)
                        .MaxAsync(x => x.SortOrder, cancellationToken) + 10 : 0), cancellationToken);
            }
            
            // Send Notification
            await _mediator.Publish(new EntityCreatedNotification<Entities.Common.Deceased>
                {CreatedId = deceased.Id, CreatedValue = deceased, UserId = userId, GoverningId = request.TargetWorkOrderId, GoverningType = nameof(WorkOrder)}, cancellationToken);
            
            // Get newly inserted entity
            var insertedEntity = await dbContext.Deceased
                .ProjectTo<DeceasedDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == deceased.Id, cancellationToken);

            return insertedEntity;
        }


    }
}