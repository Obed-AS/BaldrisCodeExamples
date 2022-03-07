using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Deceased.Deceased.Commands.Create;
using Baldris.Application.Deceased.Deceased.Commands.Update;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.Commands.UpdateOrInsert
{
    internal class UpdateOrInsertDeceasedHandler : IRequestHandler<UpdateOrInsertDeceasedCommand, DeceasedDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public UpdateOrInsertDeceasedHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<DeceasedDto> Handle(UpdateOrInsertDeceasedCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue || request.Id == Guid.Empty)
            {
                return await _mediator.Send(_mapper.Map<UpdateOrInsertDeceasedCommand, CreateDeceasedCommand>(request),
                    cancellationToken);
            }

            await _mediator.Send(_mapper.Map<UpdateOrInsertDeceasedCommand, UpdateDeceasedCommand>(request), cancellationToken);

            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            return await dbContext.Deceased
                .ProjectTo<DeceasedDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }


    }
}
