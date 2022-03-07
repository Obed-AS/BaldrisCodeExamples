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

namespace Baldris.Application.Deceased.Graves.Queries.GetPaginated
{
    internal class GetPaginatedGravesHandler : IRequestHandler<GetPaginatedGravesQuery, PaginatedItems<GraveDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedGravesHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<GraveDto>> Handle(
            GetPaginatedGravesQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.Graves.AsQueryable();

			if (request.GraveTypeId.HasValue)
			{
				query = query.Where(x => x.GraveTypeId == request.GraveTypeId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Deceased.Any(y => y.Person.DisplayName.Contains(request.SearchString)) || x.GraveType.Name.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.GraveType.Name)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<GraveDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<GraveDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
