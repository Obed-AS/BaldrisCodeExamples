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

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetByIds
{
    internal class GetCalendarEventEquipmentByIdsHandler : IRequestHandler<GetCalendarEventEquipmentByIdsQuery, IEnumerable<CalendarEventEquipmentDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventEquipmentByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventEquipmentDto>> Handle(GetCalendarEventEquipmentByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.CalendarEventEquipment
                .Where(x => request.CalendarEventEquipmentIds.Contains(x.Id))
                .OrderBy(x => x.Equipment.Name)
                .ProjectTo<CalendarEventEquipmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return entities.OrderBy(x => request.CalendarEventEquipmentIds.ToList().IndexOf(x.Id));
        }
    }
}
