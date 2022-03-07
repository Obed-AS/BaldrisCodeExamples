using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Deceased.Models;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetByIds
{
    internal class GetGrantApplicationsByIdsHandler : IRequestHandler<GetGrantApplicationsByIdsQuery, IEnumerable<GrantApplicationDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetGrantApplicationsByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GrantApplicationDto>> Handle(GetGrantApplicationsByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.GrantApplications
                .Where(x => request.GrantApplicationIds.Contains(x.Id))
                .OrderBy(x => x.Deceased.Person.DisplayName)
                .ProjectTo<GrantApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.GrantApplicationIds.ToList().IndexOf(x.Id));
        }
    }
}
