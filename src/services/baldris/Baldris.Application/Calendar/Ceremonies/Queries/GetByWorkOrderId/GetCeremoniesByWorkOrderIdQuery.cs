using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetByWorkOrderId
{
    public class GetCeremoniesByWorkOrderIdQuery : IRequest<IEnumerable<CeremonyDto>>
    {
        public GetCeremoniesByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
