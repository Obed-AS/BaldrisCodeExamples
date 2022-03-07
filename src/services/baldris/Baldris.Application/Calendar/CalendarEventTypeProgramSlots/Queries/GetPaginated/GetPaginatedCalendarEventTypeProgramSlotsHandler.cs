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

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetPaginated
{
    internal class GetPaginatedCalendarEventTypeProgramSlotsHandler : IRequestHandler<GetPaginatedCalendarEventTypeProgramSlotsQuery, PaginatedItems<CalendarEventTypeProgramSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedCalendarEventTypeProgramSlotsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CalendarEventTypeProgramSlotDto>> Handle(
            GetPaginatedCalendarEventTypeProgramSlotsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.CalendarEventTypeProgramSlots.AsQueryable();

			if (request.CalendarEventTypeId.HasValue)
			{
				query = query.Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.ProgramSlot.Name.Contains(request.SearchString) || x.CalendarEventType.Name.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.ProgramSlot.Name)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<CalendarEventTypeProgramSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<CalendarEventTypeProgramSlotDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
