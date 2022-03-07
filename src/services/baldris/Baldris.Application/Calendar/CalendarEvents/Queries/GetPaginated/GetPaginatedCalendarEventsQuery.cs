using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetPaginated
{
    public class GetPaginatedCalendarEventsQuery : IRequest<PaginatedItems<CalendarEventDto>>
    {
        public GetPaginatedCalendarEventsQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false, Guid? calendarEventTypeId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			CalendarEventTypeId = calendarEventTypeId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public Guid? CalendarEventTypeId { get; }
    }
}
