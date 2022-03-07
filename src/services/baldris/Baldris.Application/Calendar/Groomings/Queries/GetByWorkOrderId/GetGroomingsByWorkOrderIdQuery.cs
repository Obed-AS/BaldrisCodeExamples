using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.GetByWorkOrderId
{
    public class GetGroomingsByWorkOrderIdQuery : IRequest<IEnumerable<GroomingDto>>
    {
        public GetGroomingsByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
