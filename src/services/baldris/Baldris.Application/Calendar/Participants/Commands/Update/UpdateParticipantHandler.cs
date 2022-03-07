using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Participants.Commands.Update
{
    internal class UpdateParticipantHandler : IRequestHandler<UpdateParticipantCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public UpdateParticipantHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entity = _mapper.Map<UpdateParticipantCommand, Participant>(request);

            var previousValue =
                await dbContext.Participants.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(Participant), request.Id);
            }

            // Update or insert party
            if (request.UpdateOrInsertPartyCommand != null)
            {
                var party = await _mediator.Send(request.UpdateOrInsertPartyCommand, cancellationToken);
                request.PartyId = party.Id;
            }

            // Retain protected properties
            entity.CreatedAt = previousValue.CreatedAt;
            entity.CreatedBy = previousValue.CreatedBy;

            dbContext.Participants.Update(entity);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityUpdatedNotification<Participant>
                    { UpdatedId = entity.Id, CurrentValue = entity, PreviousValue = previousValue, UserId = userId },
                cancellationToken);

            return result;
        }


    }
}