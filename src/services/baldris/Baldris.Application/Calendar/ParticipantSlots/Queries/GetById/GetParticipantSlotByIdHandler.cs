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

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetById
{
    internal class GetParticipantSlotByIdHandler : IRequestHandler<GetParticipantSlotByIdQuery, ParticipantSlotDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetParticipantSlotByIdHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<ParticipantSlotDto> Handle(GetParticipantSlotByIdQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());
            var entity = await dbContext.ParticipantSlots
                .Where(x => x.Id == request.ParticipantSlotId)
                .ProjectTo<ParticipantSlotDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ParticipantSlot), request.ParticipantSlotId);
            }

            return entity;
        }
    }
}
