using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetById
{
    public class GetCalendarEventDeceasedByIdQuery : IRequest<CalendarEventDeceasedDto>
    {
        public GetCalendarEventDeceasedByIdQuery(Guid calendarEventDeceasedId)
        {
            CalendarEventDeceasedId = calendarEventDeceasedId;
        }

        public Guid CalendarEventDeceasedId { get; }
    }
}
