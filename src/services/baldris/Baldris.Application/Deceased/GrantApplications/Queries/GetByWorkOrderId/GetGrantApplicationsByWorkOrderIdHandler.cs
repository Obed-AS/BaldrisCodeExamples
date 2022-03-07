using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Deceased.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetByWorkOrderId
{
    internal class GetGrantApplicationsByWorkOrderIdHandler : IRequestHandler<GetGrantApplicationsByWorkOrderIdQuery, IEnumerable<GrantApplicationDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetGrantApplicationsByWorkOrderIdHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GrantApplicationDto>> Handle(GetGrantApplicationsByWorkOrderIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var query = dbContext.GrantApplications
                .Where(x => x.WorkOrderId == request.WorkOrderId);

            return await query
                .OrderBy(x => x.Deceased.Person.DisplayName)
                .ProjectTo<GrantApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
