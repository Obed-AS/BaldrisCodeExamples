using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetByIds
{
    public class GetCalendarEventsByIdsQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public GetCalendarEventsByIdsQuery(IEnumerable<Guid> calendarEventIds)
        {
            CalendarEventIds = calendarEventIds;
        }

        public IEnumerable<Guid> CalendarEventIds { get; }
    }
}
