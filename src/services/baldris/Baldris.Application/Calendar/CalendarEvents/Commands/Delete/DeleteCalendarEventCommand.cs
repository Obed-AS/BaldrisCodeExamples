using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Commands.Delete
{
    public class DeleteCalendarEventCommand : IRequest<int>
    {
        public DeleteCalendarEventCommand(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
