using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.Find
{
    public class FindCalendarEventDeceasedQuery : IRequest<IEnumerable<CalendarEventDeceasedDto>>
    {
        public FindCalendarEventDeceasedQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
