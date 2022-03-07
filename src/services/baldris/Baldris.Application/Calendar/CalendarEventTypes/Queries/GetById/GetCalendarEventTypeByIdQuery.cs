using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Queries.GetById
{
    public class GetCalendarEventTypeByIdQuery : IRequest<CalendarEventTypeDto>
    {
        public GetCalendarEventTypeByIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
