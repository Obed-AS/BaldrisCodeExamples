using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetByWorkOrderId
{
    public class GetGrantApplicationsByWorkOrderIdQuery : IRequest<IEnumerable<GrantApplicationDto>>
    {
        public GetGrantApplicationsByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
