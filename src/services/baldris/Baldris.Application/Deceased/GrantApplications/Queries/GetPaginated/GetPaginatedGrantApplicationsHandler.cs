using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Models;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetPaginated
{
    internal class GetPaginatedGrantApplicationsHandler : IRequestHandler<GetPaginatedGrantApplicationsQuery, PaginatedItems<GrantApplicationDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedGrantApplicationsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<GrantApplicationDto>> Handle(
            GetPaginatedGrantApplicationsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.GrantApplications.AsQueryable();

			if (request.DeceasedId.HasValue)
			{
				query = query.Where(x => x.DeceasedId == request.DeceasedId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Deceased.Person.DisplayName.Contains(request.SearchString) || x.WorkOrder.OrderCode.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.Deceased.Person.DisplayName)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<GrantApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<GrantApplicationDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
