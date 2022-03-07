using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Application.Deceased.Models;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.GrantApplications.Commands.Create
{
    internal class CreateGrantApplicationHandler : IRequestHandler<CreateGrantApplicationCommand, GrantApplicationDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateGrantApplicationHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<GrantApplicationDto> Handle(CreateGrantApplicationCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entity = _mapper.Map<CreateGrantApplicationCommand, GrantApplication>(request);

            await dbContext.GrantApplications.AddAsync(entity, cancellationToken);

            var userId = await _loggedInUserService.GetUserIdAsync();
            await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityCreatedNotification<GrantApplication>
                { CreatedId = entity.Id, CreatedValue = entity, UserId = userId }, cancellationToken);

            // Get newly inserted entity
            var insertedEntity = await dbContext.GrantApplications
                .ProjectTo<GrantApplicationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);

            return insertedEntity;
        }


    }
}