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

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetPaginated
{
    internal class GetPaginatedCalendarEventEquipmentHandler : IRequestHandler<GetPaginatedCalendarEventEquipmentQuery, PaginatedItems<CalendarEventEquipmentDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedCalendarEventEquipmentHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CalendarEventEquipmentDto>> Handle(
            GetPaginatedCalendarEventEquipmentQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.CalendarEventEquipment.AsQueryable();

            if (request.CalendarEventId.HasValue)
			{
				query = query.Where(x => x.CalendarEventId == request.CalendarEventId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Equipment.Name.Contains(request.SearchString) || x.CalendarEvent.Subject.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.Equipment.Name)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<CalendarEventEquipmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<CalendarEventEquipmentDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
