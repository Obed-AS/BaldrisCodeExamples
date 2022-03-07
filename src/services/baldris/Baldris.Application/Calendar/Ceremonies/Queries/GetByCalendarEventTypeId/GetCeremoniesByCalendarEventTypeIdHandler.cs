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

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetByCalendarEventTypeId
{
    internal class GetCeremoniesByCalendarEventTypeIdHandler : IRequestHandler<GetCeremoniesByCalendarEventTypeIdQuery, IEnumerable<CeremonyDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCeremoniesByCalendarEventTypeIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CeremonyDto>> Handle(GetCeremoniesByCalendarEventTypeIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.Ceremonies
                .Where(x => x.CalendarEventTypeId == request.CalendarEventTypeId);

            return await query
                .OrderByDescending(x => x.Start)
                .ProjectTo<CeremonyDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
