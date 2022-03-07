using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetByCalendarEventId
{
    public class GetCalendarEventDeceasedByCalendarEventIdQuery : IRequest<IEnumerable<CalendarEventDeceasedDto>>
    {
        public GetCalendarEventDeceasedByCalendarEventIdQuery(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
