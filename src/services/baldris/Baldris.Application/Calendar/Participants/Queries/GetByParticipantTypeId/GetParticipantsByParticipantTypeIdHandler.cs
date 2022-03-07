using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Participants.Queries.GetByParticipantTypeId
{
    internal class GetParticipantsByParticipantTypeIdHandler : IRequestHandler<GetParticipantsByParticipantTypeIdQuery, IEnumerable<ParticipantDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetParticipantsByParticipantTypeIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IEnumerable<ParticipantDto>> Handle(GetParticipantsByParticipantTypeIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var query = dbContext.Participants
                .Where(x => x.ParticipantTypes.Any(y => y.ParticipantTypeId ==request.ParticipantTypeId));

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
