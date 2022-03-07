using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetByIds
{
    internal class GetCalendarEventTypeProgramSlotsByIdsHandler : IRequestHandler<GetCalendarEventTypeProgramSlotsByIdsQuery, IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventTypeProgramSlotsByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventTypeProgramSlotDto>> Handle(GetCalendarEventTypeProgramSlotsByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.CalendarEventTypeProgramSlots
                .Where(x => request.CalendarEventTypeProgramSlotIds.Contains(x.Id))
                .OrderBy(x => x.ProgramSlot.Name)
                .ProjectTo<CalendarEventTypeProgramSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.CalendarEventTypeProgramSlotIds.ToList().IndexOf(x.Id));
        }
    }
}
