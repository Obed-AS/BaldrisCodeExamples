using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Commands.Delete
{
    public class DeleteCalendarEventTypeCommand : IRequest<int>
    {
        public DeleteCalendarEventTypeCommand(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
