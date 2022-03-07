using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Queries.Find
{
    public class FindCalendarEventTypesQuery : IRequest<IEnumerable<CalendarEventTypeDto>>
    {
        public FindCalendarEventTypesQuery(string searchString, bool includeSoftDeleted = false)
        {
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
