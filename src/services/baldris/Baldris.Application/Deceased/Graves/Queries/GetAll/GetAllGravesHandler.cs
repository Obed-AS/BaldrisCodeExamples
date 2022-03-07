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

namespace Baldris.Application.Deceased.Graves.Queries.GetAll
{
    internal class GetAllGravesHandler : IRequestHandler<GetAllGravesQuery, IEnumerable<GraveDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllGravesHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GraveDto>> Handle(GetAllGravesQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.Graves.AsQueryable();

            return await query
                .OrderBy(x => x.GraveType.Name)
                .ProjectTo<GraveDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
