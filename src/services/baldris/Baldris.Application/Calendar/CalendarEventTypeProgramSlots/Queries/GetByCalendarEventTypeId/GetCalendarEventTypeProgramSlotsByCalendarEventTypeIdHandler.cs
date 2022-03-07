using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetByCalendarEventTypeId
{
    internal class GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdHandler : IRequestHandler<GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdQuery, IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventTypeProgramSlotDto>> Handle(GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.CalendarEventTypeProgramSlots
                .Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);

            return await query
                .OrderBy(x => x.ProgramSlot.Name)
                .ProjectTo<CalendarEventTypeProgramSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
