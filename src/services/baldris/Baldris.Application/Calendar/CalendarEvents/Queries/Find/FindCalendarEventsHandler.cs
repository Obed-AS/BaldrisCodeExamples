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

namespace Baldris.Application.Calendar.CalendarEvents.Queries.Find
{
    internal class FindCalendarEventsHandler : IRequestHandler<FindCalendarEventsQuery, IEnumerable<CalendarEventDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public FindCalendarEventsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventDto>> Handle(FindCalendarEventsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.CalendarEvents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Subject.Contains(request.SearchString) || x.Text.Contains(request.SearchString));
            }

            return await query
                .OrderByDescending(x => x.Start)
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
