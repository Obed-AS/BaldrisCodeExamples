using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetByCalendarEventId
{
    public class GetCalendarEventUsersByCalendarEventIdQuery : IRequest<IEnumerable<CalendarEventUserDto>>
    {
        public GetCalendarEventUsersByCalendarEventIdQuery(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
