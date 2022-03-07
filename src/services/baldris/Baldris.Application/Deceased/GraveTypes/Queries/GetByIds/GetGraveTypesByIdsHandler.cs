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

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetByIds
{
    internal class GetGraveTypesByIdsHandler : IRequestHandler<GetGraveTypesByIdsQuery, IEnumerable<GraveTypeDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetGraveTypesByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GraveTypeDto>> Handle(GetGraveTypesByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.GraveTypes
                .Where(x => request.GraveTypeIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ProjectTo<GraveTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.GraveTypeIds.ToList().IndexOf(x.Id));
        }
    }
}
