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

namespace Baldris.Application.Calendar.Groomings.Queries.GetByIds
{
    internal class GetGroomingsByIdsHandler : IRequestHandler<GetGroomingsByIdsQuery, IEnumerable<GroomingDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetGroomingsByIdsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<GroomingDto>> Handle(GetGroomingsByIdsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var entities = await dbContext.Groomings
                .Where(x => request.GroomingIds.Contains(x.Id))
                .OrderBy(x => x.Start)
                .ProjectTo<GroomingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return entities.OrderBy(x => request.GroomingIds.ToList().IndexOf(x.Id));
        }
    }
}
