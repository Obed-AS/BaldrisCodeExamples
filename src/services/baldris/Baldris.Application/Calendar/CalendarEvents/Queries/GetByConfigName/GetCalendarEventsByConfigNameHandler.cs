using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.BaldrisConfigs.Queries.GetBaldrisConfigsByName;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetByConfigName
{
    internal class GetCalendarEventsByConfigNameHandler : IRequestHandler<GetCalendarEventsByConfigNameQuery, IEnumerable<CalendarEventDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public GetCalendarEventsByConfigNameHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<IEnumerable<CalendarEventDto>> Handle(GetCalendarEventsByConfigNameQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var configs = (await _mediator.Send(new GetBaldrisConfigsByNameQuery(request.ConfigName, false, null,
                request.DepartmentId, request.LegalUnitId, request.WorkOrderTypeId), cancellationToken)).ToList();

            if (!configs.Any())
            {
                return Array.Empty<CalendarEventDto>();
            }

            var configTargetIds = configs.Where(x => x.CustomFieldData?.DataTargetId != null)
                .Select(x => x.CustomFieldData.DataTargetId.Value);

            var query = dbContext.CalendarEvents.AsQueryable();

            if (configs.First().CustomFieldData.DataType == CustomFieldDataType.CalendarEventType)
            {
                query = query.Where(x => x.CalendarEventTypeId.HasValue && configTargetIds.Contains(x.CalendarEventTypeId.Value));
            }
            else
            {
                query = query.Where(x => configTargetIds.Contains(x.Id));
            }

            return await query
                .OrderByDescending(x => x.Start)
                .ProjectTo<CalendarEventDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
