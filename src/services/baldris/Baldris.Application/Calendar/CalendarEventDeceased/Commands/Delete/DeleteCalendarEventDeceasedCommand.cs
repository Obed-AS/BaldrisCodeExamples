using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Commands.Delete
{
    public class DeleteCalendarEventDeceasedCommand : IRequest<int>
    {
        public DeleteCalendarEventDeceasedCommand(Guid calendarEventDeceasedId)
        {
            CalendarEventDeceasedId = calendarEventDeceasedId;
        }

        public Guid CalendarEventDeceasedId { get; }
    }
}
