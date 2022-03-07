using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Baldris.Application.BaldrisConfigs.Queries.GetBaldrisConfigsByName;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Participants.Queries.GetByConfigName
{
    internal class GetParticipantsByConfigNameHandler : IRequestHandler<GetParticipantsByConfigNameQuery, IEnumerable<ParticipantDto>>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public GetParticipantsByConfigNameHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ParticipantDto>> Handle(GetParticipantsByConfigNameQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var configs = (await _mediator.Send(new GetBaldrisConfigsByNameQuery(request.ConfigName, false, null,
                request.DepartmentId, request.LegalUnitId, request.WorkOrderTypeId), cancellationToken)).ToList();

            if (!configs.Any())
            {
                return Array.Empty<ParticipantDto>();
            }

            var configTargetIds = configs.Where(x => x.CustomFieldData?.DataTargetId != null)
                .Select(x => x.CustomFieldData.DataTargetId.Value);

            var query = dbContext.Participants.AsQueryable();

			if (!request.IncludeSoftDeleted)
			{
				query = query.Where(x => !x.IsDeleted);
			}

            if (configs.First().CustomFieldData.DataType == CustomFieldDataType.ParticipantType)
            {
                query = query.Where(x => x.ParticipantTypes.Any(y => configTargetIds.Contains(y.ParticipantTypeId)));
            }
            else
            {
                query = query.Where(x => configTargetIds.Contains(x.Id));
            }

            return await query
                .OrderBy(x => x.Party.DisplayName)
                .ProjectTo<ParticipantDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }


    }
}
