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

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetAll
{
    internal class GetAllGraveTypesHandler : IRequestHandler<GetAllGraveTypesQuery, IEnumerable<GraveTypeDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllGraveTypesHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GraveTypeDto>> Handle(GetAllGraveTypesQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.GraveTypes.AsQueryable();

			if (!request.IncludeSoftDeleted)
			{
				query = query.Where(x => !x.IsDeleted);
			}

            return await query
                .OrderBy(x => x.Name)
                .ProjectTo<GraveTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
