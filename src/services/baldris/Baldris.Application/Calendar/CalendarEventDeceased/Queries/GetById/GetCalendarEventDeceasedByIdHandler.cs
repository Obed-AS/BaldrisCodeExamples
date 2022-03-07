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

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetById
{
    internal class GetCalendarEventDeceasedByIdHandler : IRequestHandler<GetCalendarEventDeceasedByIdQuery, CalendarEventDeceasedDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetCalendarEventDeceasedByIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<CalendarEventDeceasedDto> Handle(GetCalendarEventDeceasedByIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var entity = await dbContext.CalendarEventDeceased
                .Where(x => x.Id == request.CalendarEventDeceasedId)
                .ProjectTo<CalendarEventDeceasedDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CalendarEventDeceased), request.CalendarEventDeceasedId);
            }

            return entity;
        }
    }
}
