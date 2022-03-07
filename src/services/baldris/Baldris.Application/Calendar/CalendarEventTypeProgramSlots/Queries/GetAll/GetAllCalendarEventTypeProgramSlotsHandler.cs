using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetAll
{
    internal class GetAllCalendarEventTypeProgramSlotsHandler : IRequestHandler<GetAllCalendarEventTypeProgramSlotsQuery, IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllCalendarEventTypeProgramSlotsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventTypeProgramSlotDto>> Handle(GetAllCalendarEventTypeProgramSlotsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.CalendarEventTypeProgramSlots.AsQueryable();

            return await query
                .OrderBy(x => x.ProgramSlot.Name)
                .ProjectTo<CalendarEventTypeProgramSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
