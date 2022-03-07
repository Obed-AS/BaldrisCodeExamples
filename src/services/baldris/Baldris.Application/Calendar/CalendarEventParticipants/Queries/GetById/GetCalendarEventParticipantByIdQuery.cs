using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.GetById
{
    public class GetCalendarEventParticipantByIdQuery : IRequest<CalendarEventParticipantDto>
    {
        public GetCalendarEventParticipantByIdQuery(Guid calendarEventParticipantId)
        {
            CalendarEventParticipantId = calendarEventParticipantId;
        }

        public Guid CalendarEventParticipantId { get; }
    }
}
