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

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetPaginated
{
    internal class GetPaginatedParticipantSlotsHandler : IRequestHandler<GetPaginatedParticipantSlotsQuery, PaginatedItems<ParticipantSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetPaginatedParticipantSlotsHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<ParticipantSlotDto>> Handle(
            GetPaginatedParticipantSlotsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.ParticipantSlots.AsQueryable();

			if (request.CalendarEventTypeId.HasValue)
			{
				query = query.Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);
			}

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.ParticipantRole.Name.Contains(request.SearchString) || x.CalendarEventType.Name.Contains(request.SearchString));
            }

            var totalItems = await query.LongCountAsync(cancellationToken);

            query = query
                .OrderBy(x => x.ParticipantRole.Name)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);


            var itemsOnPage = await query.ProjectTo<ParticipantSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedItems<ParticipantSlotDto>(request.PageIndex, request.PageSize,
                totalItems, itemsOnPage);
        }
    }
}
