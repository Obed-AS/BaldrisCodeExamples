using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetByWorkOrderId
{
    public class GetDeceasedByWorkOrderIdQuery : IRequest<IEnumerable<DeceasedDto>>
    {
        public GetDeceasedByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
