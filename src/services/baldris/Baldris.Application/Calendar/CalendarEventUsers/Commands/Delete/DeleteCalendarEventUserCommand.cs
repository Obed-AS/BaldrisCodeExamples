using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Commands.Delete
{
    public class DeleteCalendarEventUserCommand : IRequest<int>
    {
        public DeleteCalendarEventUserCommand(Guid calendarEventUserId)
        {
            CalendarEventUserId = calendarEventUserId;
        }

        public Guid CalendarEventUserId { get; }
    }
}
