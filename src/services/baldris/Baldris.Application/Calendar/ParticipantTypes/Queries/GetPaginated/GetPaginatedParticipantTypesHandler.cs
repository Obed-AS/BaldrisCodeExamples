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

namespace Baldris.Application.Calendar.ParticipantTypes.Queries.GetPaginated
{
    internal class GetPaginatedParticipantTypesHandler : IRequestHandler<GetPaginatedParticipantTypesQuery, PaginatedItems<ParticipantTypeDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedParticipantTypesHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<ParticipantTypeDto>> Handle(
            GetPaginatedParticipantTypesQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.ParticipantTypes.AsQueryable();

			if (!request.IncludeSoftDeleted)
			{
				query = query.Where(x => !x.IsDeleted);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.SearchString) || x.Description.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.Name)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<ParticipantTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<ParticipantTypeDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
