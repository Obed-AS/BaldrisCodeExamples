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

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.GetByIds
{
    internal class GetCalendarEventParticipantsByIdsHandler : IRequestHandler<GetCalendarEventParticipantsByIdsQuery, IEnumerable<CalendarEventParticipantDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventParticipantsByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventParticipantDto>> Handle(GetCalendarEventParticipantsByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.CalendarEventParticipants
                .Where(x => request.CalendarEventParticipantIds.Contains(x.Id))
                .OrderBy(x => x.Participant.Party.DisplayName)
                .ProjectTo<CalendarEventParticipantDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return entities.OrderBy(x => request.CalendarEventParticipantIds.ToList().IndexOf(x.Id));
        }
    }
}
