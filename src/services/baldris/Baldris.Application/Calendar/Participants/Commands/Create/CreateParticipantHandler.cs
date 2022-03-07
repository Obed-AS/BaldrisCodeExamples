using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Participants.Commands.Create
{
    internal class CreateParticipantHandler : IRequestHandler<CreateParticipantCommand, ParticipantDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateParticipantHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<ParticipantDto> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entity = _mapper.Map<CreateParticipantCommand, Participant>(request);

            await dbContext.Participants.AddAsync(entity, cancellationToken);
            
            // Update or insert party
            if (request.UpdateOrInsertPartyCommand != null)
            {
                var party = await _mediator.Send(request.UpdateOrInsertPartyCommand, cancellationToken);
                request.PartyId = party.Id;
            }

            var userId = await _loggedInUserService.GetUserIdAsync();
            await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityCreatedNotification<Participant>
                { CreatedId = entity.Id, CreatedValue = entity, UserId = userId }, cancellationToken);

            // Get newly inserted entity
            var insertedEntity = await dbContext.Participants
                .ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);

            return insertedEntity;
        }


    }
}