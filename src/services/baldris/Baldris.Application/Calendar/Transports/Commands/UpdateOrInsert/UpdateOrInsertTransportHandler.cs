using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.Transports.Commands.Create;
using Baldris.Application.Calendar.Transports.Commands.Update;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Calendar.Transports.Commands.UpdateOrInsert
{
    internal class UpdateOrInsertTransportHandler : IRequestHandler<UpdateOrInsertTransportCommand, TransportDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public UpdateOrInsertTransportHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<TransportDto> Handle(UpdateOrInsertTransportCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue || request.Id == Guid.Empty)
            {
                return await _mediator.Send(_mapper.Map<UpdateOrInsertTransportCommand, CreateTransportCommand>(request),
                    cancellationToken);
            }

            await _mediator.Send(_mapper.Map<UpdateOrInsertTransportCommand, UpdateTransportCommand>(request), cancellationToken);

            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            return await dbContext.Transports
                .ProjectTo<TransportDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }


    }
}
