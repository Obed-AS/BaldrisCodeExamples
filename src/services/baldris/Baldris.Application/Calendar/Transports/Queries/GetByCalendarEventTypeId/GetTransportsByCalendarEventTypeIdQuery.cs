using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.GetByCalendarEventTypeId
{
    public class GetTransportsByCalendarEventTypeIdQuery : IRequest<IEnumerable<TransportDto>>
    {
        public GetTransportsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
