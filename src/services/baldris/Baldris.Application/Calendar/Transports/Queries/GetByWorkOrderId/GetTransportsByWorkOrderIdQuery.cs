using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.GetByWorkOrderId
{
    public class GetTransportsByWorkOrderIdQuery : IRequest<IEnumerable<TransportDto>>
    {
        public GetTransportsByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
