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

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetByCalendarEventId
{
    internal class GetCalendarEventDeceasedByCalendarEventIdHandler : IRequestHandler<GetCalendarEventDeceasedByCalendarEventIdQuery, IEnumerable<CalendarEventDeceasedDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventDeceasedByCalendarEventIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventDeceasedDto>> Handle(GetCalendarEventDeceasedByCalendarEventIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.CalendarEventDeceased
                .Where(x => x.CalendarEventId == request.CalendarEventId);

            return await query
                .OrderBy(x => x.Deceased.Person.DisplayName)
                .ProjectTo<CalendarEventDeceasedDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
