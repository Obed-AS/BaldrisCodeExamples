using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetByCalendarEventTypeId
{
    public class GetCalendarEventsByCalendarEventTypeIdQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public GetCalendarEventsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
