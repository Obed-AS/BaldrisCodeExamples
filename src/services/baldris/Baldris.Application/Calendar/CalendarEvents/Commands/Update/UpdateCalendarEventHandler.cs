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

namespace Baldris.Application.Calendar.CalendarEvents.Commands.Update
{
    internal class UpdateCalendarEventHandler : IRequestHandler<UpdateCalendarEventCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public UpdateCalendarEventHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(UpdateCalendarEventCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entity = _mapper.Map<UpdateCalendarEventCommand, CalendarEvent>(request);

            var previousValue =
                await dbContext.CalendarEvents.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(CalendarEvent), request.Id);
            }

            // Retain protected properties
            entity.CreatedAt = previousValue.CreatedAt;
            entity.CreatedBy = previousValue.CreatedBy;
            entity.AttendeesSerialized = previousValue.AttendeesSerialized;
            entity.DeceasedSerialized = previousValue.DeceasedSerialized;
            entity.HostSerialized = previousValue.HostSerialized;

            dbContext.CalendarEvents.Update(entity);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            await _mediator.Publish(new EntityUpdatedNotification<CalendarEvent>
                    { UpdatedId = entity.Id, CurrentValue = entity, PreviousValue = previousValue, UserId = userId, GoverningId = request.WorkOrderId, GoverningType = nameof(WorkOrder) },
                cancellationToken);

            return result;
        }


    }
}