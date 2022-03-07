using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.Find
{
    public class FindCalendarEventsQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public FindCalendarEventsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
