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

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetPaginated
{
    internal class GetPaginatedCalendarEventUsersHandler : IRequestHandler<GetPaginatedCalendarEventUsersQuery, PaginatedItems<CalendarEventUserDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedCalendarEventUsersHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CalendarEventUserDto>> Handle(
            GetPaginatedCalendarEventUsersQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.CalendarEventUsers.AsQueryable();

			if (request.CalendarEventId.HasValue)
			{
				query = query.Where(x => x.CalendarEventId == request.CalendarEventId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.User.DisplayName.Contains(request.SearchString) || x.User.Email.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.User.DisplayName)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<CalendarEventUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<CalendarEventUserDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
