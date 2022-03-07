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

namespace Baldris.Application.Calendar.Participants.Queries.GetAll
{
    internal class GetAllParticipantsHandler : IRequestHandler<GetAllParticipantsQuery, IEnumerable<ParticipantDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllParticipantsHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ParticipantDto>> Handle(GetAllParticipantsQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.Participants.AsQueryable();

			if (!request.IncludeSoftDeleted)
			{
				query = query.Where(x => !x.IsDeleted);
			}

            return await query
                .OrderBy(x => x.Party.DisplayName)
                .ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
