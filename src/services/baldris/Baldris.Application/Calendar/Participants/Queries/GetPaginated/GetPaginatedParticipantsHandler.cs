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

namespace Baldris.Application.Calendar.Participants.Queries.GetPaginated
{
    internal class GetPaginatedParticipantsHandler : IRequestHandler<GetPaginatedParticipantsQuery, PaginatedItems<ParticipantDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedParticipantsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<ParticipantDto>> Handle(
            GetPaginatedParticipantsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.Participants.AsQueryable();

			if (!request.IncludeSoftDeleted)
			{
				query = query.Where(x => !x.IsDeleted);
			}

			if (request.ParticipantTypeId.HasValue)
			{
				query = query.Where(x => x.ParticipantTypes.Any(y => y.ParticipantTypeId ==request.ParticipantTypeId));
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.Party.DisplayName.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.Party.DisplayName)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<ParticipantDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
