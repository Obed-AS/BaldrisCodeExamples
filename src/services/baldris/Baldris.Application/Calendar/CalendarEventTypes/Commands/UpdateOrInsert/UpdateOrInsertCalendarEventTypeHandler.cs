using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventTypes.Commands.Create;
using Baldris.Application.Calendar.CalendarEventTypes.Commands.Update;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEventTypes.Commands.UpdateOrInsert
{
    internal class UpdateOrInsertCalendarEventTypeHandler : IRequestHandler<UpdateOrInsertCalendarEventTypeCommand, CalendarEventTypeDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public UpdateOrInsertCalendarEventTypeHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<CalendarEventTypeDto> Handle(UpdateOrInsertCalendarEventTypeCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue || request.Id == Guid.Empty)
            {
                return await _mediator.Send(_mapper.Map<UpdateOrInsertCalendarEventTypeCommand, CreateCalendarEventTypeCommand>(request),
                    cancellationToken);
            }

            await _mediator.Send(_mapper.Map<UpdateOrInsertCalendarEventTypeCommand, UpdateCalendarEventTypeCommand>(request), cancellationToken);

            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            return await dbContext.CalendarEventTypes
                .ProjectTo<CalendarEventTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }


    }
}
