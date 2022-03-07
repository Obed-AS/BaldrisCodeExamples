using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Queries.GetAll
{
    public class GetAllCalendarEventTypesQuery : IRequest<IEnumerable<CalendarEventTypeDto>>
    {
        public GetAllCalendarEventTypesQuery(bool includeSoftDeleted = false)
		{
			IncludeSoftDeleted = includeSoftDeleted;
		}

		public bool IncludeSoftDeleted { get; }
    }
}
