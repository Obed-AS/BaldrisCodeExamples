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

namespace Baldris.Application.Calendar.CalendarEvents.Commands.Create
{
    internal class CreateCalendarEventHandler : IRequestHandler<CreateCalendarEventCommand, CalendarEventDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateCalendarEventHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<CalendarEventDto> Handle(CreateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entity = _mapper.Map<CreateCalendarEventCommand, CalendarEvent>(request);

            await dbContext.CalendarEvents.AddAsync(entity, cancellationToken);

            var userId = await _loggedInUserService.GetUserIdAsync();
            await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityCreatedNotification<CalendarEvent>
                { CreatedId = entity.Id, CreatedValue = entity, UserId = userId, GoverningId = request.WorkOrderId, GoverningType = nameof(WorkOrder) }, cancellationToken);

            // Get newly inserted entity
            var insertedEntity = await dbContext.CalendarEvents
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);

            return insertedEntity;
        }


    }
}