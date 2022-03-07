using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetById
{
    public class GetCalendarEventUserByIdQuery : IRequest<CalendarEventUserDto>
    {
        public GetCalendarEventUserByIdQuery(Guid calendarEventUserId)
        {
            CalendarEventUserId = calendarEventUserId;
        }

        public Guid CalendarEventUserId { get; }
    }
}
