using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.GetByCalendarEventTypeId
{
    public class GetGroomingsByCalendarEventTypeIdQuery : IRequest<IEnumerable<GroomingDto>>
    {
        public GetGroomingsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
