using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetByIds
{
    internal class GetParticipantSlotsByIdsHandler : IRequestHandler<GetParticipantSlotsByIdsQuery, IEnumerable<ParticipantSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetParticipantSlotsByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ParticipantSlotDto>> Handle(GetParticipantSlotsByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.ParticipantSlots
                .Where(x => request.ParticipantSlotIds.Contains(x.Id))
                .OrderBy(x => x.ParticipantRole.Name)
                .ProjectTo<ParticipantSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.ParticipantSlotIds.ToList().IndexOf(x.Id));
        }
    }
}
