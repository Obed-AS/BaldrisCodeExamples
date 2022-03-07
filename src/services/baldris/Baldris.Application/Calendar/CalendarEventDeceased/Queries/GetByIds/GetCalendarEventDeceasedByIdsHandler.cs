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

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetByIds
{
    internal class GetCalendarEventDeceasedByIdsHandler : IRequestHandler<GetCalendarEventDeceasedByIdsQuery, IEnumerable<CalendarEventDeceasedDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventDeceasedByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventDeceasedDto>> Handle(GetCalendarEventDeceasedByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.CalendarEventDeceased
                .Where(x => request.CalendarEventDeceasedIds.Contains(x.Id))
                .OrderBy(x => x.Deceased.Person.DisplayName)
                .ProjectTo<CalendarEventDeceasedDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return entities.OrderBy(x => request.CalendarEventDeceasedIds.ToList().IndexOf(x.Id));
        }
    }
}
