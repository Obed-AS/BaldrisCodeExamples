using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Deceased.GraveTypes.Commands.Create;
using Baldris.Application.Deceased.GraveTypes.Commands.Update;
using MediatR;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.GraveTypes.Commands.UpdateOrInsert
{
    internal class UpdateOrInsertGraveTypeHandler : IRequestHandler<UpdateOrInsertGraveTypeCommand, GraveTypeDto>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public UpdateOrInsertGraveTypeHandler(IBaldrisDbContextFactory dbContextFactory, IMapper mapper,
            ITenantService tenantService, IMediator mediator)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<GraveTypeDto> Handle(UpdateOrInsertGraveTypeCommand request, CancellationToken cancellationToken)
        {
            if (!request.Id.HasValue || request.Id == Guid.Empty)
            {
                return await _mediator.Send(_mapper.Map<UpdateOrInsertGraveTypeCommand, CreateGraveTypeCommand>(request),
                    cancellationToken);
            }

            await _mediator.Send(_mapper.Map<UpdateOrInsertGraveTypeCommand, UpdateGraveTypeCommand>(request), cancellationToken);

            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            return await dbContext.GraveTypes
                .ProjectTo<GraveTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }


    }
}
