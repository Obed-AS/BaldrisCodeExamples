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

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetByIds
{
    internal class GetCalendarEventUsersByIdsHandler : IRequestHandler<GetCalendarEventUsersByIdsQuery, IEnumerable<CalendarEventUserDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventUsersByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<CalendarEventUserDto>> Handle(GetCalendarEventUsersByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.CalendarEventUsers
                .Where(x => request.CalendarEventUserIds.Contains(x.Id))
                .OrderBy(x => x.User.DisplayName)
                .ProjectTo<CalendarEventUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.CalendarEventUserIds.ToList().IndexOf(x.Id));
        }
    }
}
