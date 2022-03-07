using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetById
{
    public class GetCalendarEventByIdQuery : IRequest<CalendarEventDto>
    {
        public GetCalendarEventByIdQuery(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
