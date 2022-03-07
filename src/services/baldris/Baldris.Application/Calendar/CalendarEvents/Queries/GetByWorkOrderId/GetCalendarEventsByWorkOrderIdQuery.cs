using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetByWorkOrderId
{
    public class GetCalendarEventsByWorkOrderIdQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public GetCalendarEventsByWorkOrderIdQuery(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public Guid WorkOrderId { get; }
    }
}
