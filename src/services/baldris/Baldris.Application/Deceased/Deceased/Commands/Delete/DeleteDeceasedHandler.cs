using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baldris.Application.Common.Exceptions;
using Baldris.Application.Common.Interfaces;
using Baldris.Application.Common.Notifications;
using Baldris.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Interfaces;

namespace Baldris.Application.Deceased.Deceased.Commands.Delete
{
    internal class DeleteDeceasedHandler : IRequestHandler<DeleteDeceasedCommand, int>
    {
        private readonly IBaldrisDbContextFactory _dbContextFactory;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteDeceasedHandler(IBaldrisDbContextFactory dbContextFactory,
            ITenantService tenantService, IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _dbContextFactory = dbContextFactory;
            _tenantService = tenantService;
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<int> Handle(DeleteDeceasedCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _dbContextFactory.GetDbContext(await _tenantService.GetCurrentTenantAsync());

            var previousValue =
                await dbContext.Deceased
                    .Include(x => x.GrantApplications)
                    .Include(x => x.WorkOrders)
                    .Include(x => x.Events)
                    .FirstOrDefaultAsync(x => x.Id == request.DeceasedId, cancellationToken);

            if (previousValue is null)
            {
                throw new NotFoundException(nameof(Entities.Common.Deceased), request.DeceasedId);
            }

            dbContext.Deceased.Remove(previousValue);

            var userId = await _loggedInUserService.GetUserIdAsync();
            var result = await dbContext.SaveChangesAsync(userId, cancellationToken);

            // Send Notification
            if (previousValue.WorkOrders != null && previousValue.WorkOrders.Any())
            {
                foreach (var workOrderDeceased in previousValue.WorkOrders)
                {
                    await _mediator.Publish(new EntityDeletedNotification<WorkOrderDeceased>
                        {
                            DeletedId = workOrderDeceased.Id, DeletedValue = workOrderDeceased, UserId = userId,
                            GoverningId = workOrderDeceased.WorkOrderId, GoverningType = nameof(WorkOrder)
                        },
                        cancellationToken);
                }
            }
            await _mediator.Publish(new EntityDeletedNotification<Entities.Common.Deceased>
                    { DeletedId = previousValue.Id, DeletedValue = previousValue, UserId = userId },
                cancellationToken);

            return result;
        }


    }
}
