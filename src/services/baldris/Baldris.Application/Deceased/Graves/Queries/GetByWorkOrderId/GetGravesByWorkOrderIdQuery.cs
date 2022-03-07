using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetByWorkOrderId
{
    public class GetGravesByWorkOrderIdQuery : IRequest<IEnumerable<GraveDto>>
    {
        public GetGravesByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
