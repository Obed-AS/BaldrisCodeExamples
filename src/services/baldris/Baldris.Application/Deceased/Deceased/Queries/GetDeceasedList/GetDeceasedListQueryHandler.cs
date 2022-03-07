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

namespace Baldris.Application.Deceased.Deceased.Queries.GetDeceasedList {
    internal class GetDeceasedListQueryHandler : IRequestHandler<GetDeceasedListQuery, IList<DeceasedListItemDto>> {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetDeceasedListQueryHandler(IBaldrisDbContextFactory dbContextFactory, ITenantService tenantService, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mapper = mapper;
        }
        public async Task<IList<DeceasedListItemDto>> Handle(GetDeceasedListQuery request, CancellationToken cancellationToken) {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            
            var query = dbContext.Deceased.AsQueryable();

            if (request.DateOfDeathStartDate.HasValue && request.DateOfDeathEndDate.HasValue) {
                query = query.Where(d => d.Person.DateOfDeath >= request.DateOfDeathStartDate.Value.Date && d.Person.DateOfDeath <= request.DateOfDeathEndDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(request.PlaceOfBirthSearchString)) {
                query = query.Where(d => d.MunicipalityOfBirth.Name.Contains(request.PlaceOfBirthSearchString));
            }

            if (!string.IsNullOrWhiteSpace(request.PlaceOfDeathSearchString)) {
                query = query.Where(d => d.PlaceOfDeath.Name.Contains(request.PlaceOfDeathSearchString) || d.MunicipalityOfDeath.Name.Contains(request.PlaceOfDeathSearchString));
            }

            return await query.OrderByDescending(d => d.CreatedAt)
                .ProjectTo<DeceasedListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}