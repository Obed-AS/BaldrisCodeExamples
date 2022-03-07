using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.GetByCalendarEventId
{
    public class GetCalendarEventParticipantsByCalendarEventIdQuery : IRequest<IEnumerable<CalendarEventParticipantDto>>
    {
        public GetCalendarEventParticipantsByCalendarEventIdQuery(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
