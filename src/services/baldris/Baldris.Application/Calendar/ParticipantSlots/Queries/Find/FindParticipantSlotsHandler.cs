using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.Find
{
    internal class FindParticipantSlotsHandler : IRequestHandler<FindParticipantSlotsQuery, IEnumerable<ParticipantSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public FindParticipantSlotsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ParticipantSlotDto>> Handle(FindParticipantSlotsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.ParticipantSlots.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                query = query.Where(x =>
                    x.ParticipantRole.Name.Contains(request.SearchString) || x.CalendarEventType.Name.Contains(request.SearchString));
            }

            return await query
                .OrderBy(x => x.ParticipantRole.Name)
                .ProjectTo<ParticipantSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
