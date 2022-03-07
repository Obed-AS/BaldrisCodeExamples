using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetByCalendarEventTypeId
{
    internal class GetParticipantSlotsByCalendarEventTypeIdHandler : IRequestHandler<GetParticipantSlotsByCalendarEventTypeIdQuery, IEnumerable<ParticipantSlotDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetParticipantSlotsByCalendarEventTypeIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ParticipantSlotDto>> Handle(GetParticipantSlotsByCalendarEventTypeIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.ParticipantSlots
                .Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);

            return await query
                .OrderBy(x => x.ParticipantRole.Name)
                .ProjectTo<ParticipantSlotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
