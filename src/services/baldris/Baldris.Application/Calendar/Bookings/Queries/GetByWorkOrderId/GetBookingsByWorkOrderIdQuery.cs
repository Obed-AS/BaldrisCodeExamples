using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetByWorkOrderId
{
    public class GetBookingsByWorkOrderIdQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetBookingsByWorkOrderIdQuery(Guid workOrderId, bool includeSoftDeleted = false)
        {
            WorkOrderId = workOrderId;
            IncludeSoftDeleted = includeSoftDeleted;
        }

        public Guid WorkOrderId { get; }
        public bool IncludeSoftDeleted { get; }
    }
}
