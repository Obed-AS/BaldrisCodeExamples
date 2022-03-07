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

namespace Baldris.Application.Calendar.Groomings.Queries.GetByWorkOrderId
{
    internal class GetGroomingsByWorkOrderIdHandler : IRequestHandler<GetGroomingsByWorkOrderIdQuery, IEnumerable<GroomingDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetGroomingsByWorkOrderIdHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroomingDto>> Handle(GetGroomingsByWorkOrderIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.Groomings
                .Where(x => x.WorkOrderId == request.WorkOrderId);

            return await query
                .OrderBy(x => x.Start)
                .ProjectTo<GroomingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
