using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Deceased.Models;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Graves.Queries.GetByGraveTypeId
{
    internal class GetGravesByGraveTypeIdHandler : IRequestHandler<GetGravesByGraveTypeIdQuery, IEnumerable<GraveDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetGravesByGraveTypeIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GraveDto>> Handle(GetGravesByGraveTypeIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.Graves
                .Where(x => x.GraveTypeId == request.GraveTypeId);

            return await query
                .OrderBy(x => x.GraveType.Name)
                .ProjectTo<GraveDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
