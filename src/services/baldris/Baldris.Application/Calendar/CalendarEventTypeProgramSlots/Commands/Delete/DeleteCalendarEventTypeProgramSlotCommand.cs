using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.Delete
{
    public class DeleteCalendarEventTypeProgramSlotCommand : IRequest<int>
    {
        public DeleteCalendarEventTypeProgramSlotCommand(Guid calendarEventTypeProgramSlotId)
        {
            CalendarEventTypeProgramSlotId = calendarEventTypeProgramSlotId;
        }

        public Guid CalendarEventTypeProgramSlotId { get; }
    }
}
