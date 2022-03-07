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

namespace Baldris.Application.Deceased.Deceased.Queries.GetPaginated
{
    internal class GetPaginatedDeceasedHandler : IRequestHandler<GetPaginatedDeceasedQuery, PaginatedItems<DeceasedDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedDeceasedHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<DeceasedDto>> Handle(
            GetPaginatedDeceasedQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.Deceased.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Person.DisplayName.Contains(request.SearchString) || x.Person.FirstName.Contains(request.SearchString) || x.Person.LastName.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.Person.DisplayName)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<DeceasedDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<DeceasedDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
