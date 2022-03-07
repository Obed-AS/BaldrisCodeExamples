using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Commands.Delete
{
    public class DeleteCalendarEventParticipantCommand : IRequest<int>
    {
        public DeleteCalendarEventParticipantCommand(Guid calendarEventParticipantId)
        {
            CalendarEventParticipantId = calendarEventParticipantId;
        }

        public Guid CalendarEventParticipantId { get; }
    }
}
