using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetPaginated
{
    internal class GetPaginatedCalendarEventsHandler : IRequestHandler<GetPaginatedCalendarEventsQuery, PaginatedItems<CalendarEventDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedCalendarEventsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CalendarEventDto>> Handle(
            GetPaginatedCalendarEventsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.CalendarEvents.AsQueryable();

			if (request.CalendarEventTypeId.HasValue)
			{
				query = query.Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Subject.Contains(request.SearchString) || x.Text.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderByDescending(x => x.Start)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<CalendarEventDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
