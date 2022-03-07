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

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetByCalendarEventId
{
    internal class GetCalendarEventEquipmentByCalendarEventIdHandler : IRequestHandler<GetCalendarEventEquipmentByCalendarEventIdQuery, IEnumerable<CalendarEventEquipmentDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventEquipmentByCalendarEventIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventEquipmentDto>> Handle(GetCalendarEventEquipmentByCalendarEventIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.CalendarEventEquipment
                .Where(x => x.CalendarEventId == request.CalendarEventId);

            return await query
                .OrderBy(x => x.Equipment.Name)
                .ProjectTo<CalendarEventEquipmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
