using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Application.Parties.Persons.Commands.UpdateOrInsertPerson;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.Commands.Update
{
    internal class UpdateDeceasedHandler : IRequestHandler<UpdateDeceasedCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public UpdateDeceasedHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(UpdateDeceasedCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            
            var previousValue =
                await dbContext.Deceased.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(Entities.Common.Deceased), request.Id);
            }

            var entity = _mapper.Map<UpdateDeceasedCommand, Entities.Common.Deceased>(request);
            
            var updateOrInsertPersonCommand = new UpdateOrInsertPersonCommand
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

            var person = await _mediator.Send(updateOrInsertPersonCommand, cancellationToken);
            entity.PersonId = person.Id;
            
            // Extract booleans from options
            entity.IsToBeCremated = entity.Options.HasFlag(DeceasedOptions.IsToBeCremated);
            entity.IsAuthoritiesNoticedOfCremation = entity.Options.HasFlag(DeceasedOptions.IsAuthoritiesNoticedOfCremation);
            entity.IsApplyingForAshSpreading = entity.Options.HasFlag(DeceasedOptions.IsApplyingForAshSpreading);
            entity.IsFamilyAttendingUrnLowering = entity.Options.HasFlag(DeceasedOptions.IsFamilyAttendingUrnLowering);
            entity.IsTheRemainsToBeViewed = entity.Options.HasFlag(DeceasedOptions.IsTheRemainsToBeViewed);
            entity.IsOutOfTown = entity.Options.HasFlag(DeceasedOptions.IsOutOfTown);
            entity.WasStillborn = entity.Options.HasFlag(DeceasedOptions.WasStillborn);
            entity.IsGraveRegistrar = entity.Options.HasFlag(DeceasedOptions.IsGraveRegistrar);
            entity.IsGovernmentEmployee = entity.Options.HasFlag(DeceasedOptions.IsGovernmentEmployee);
            entity.IsToBeShipped = entity.Options.HasFlag(DeceasedOptions.IsToBeShipped);

            dbContext.Deceased.Update(entity);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityUpdatedNotification<Entities.Common.Deceased>
                    { UpdatedId = entity.Id, CurrentValue = entity, PreviousValue = previousValue, UserId = userId, GoverningId = request.TargetWorkOrderId, GoverningType = nameof(WorkOrder)},
                cancellationToken);

            return result;
        }


    }
}