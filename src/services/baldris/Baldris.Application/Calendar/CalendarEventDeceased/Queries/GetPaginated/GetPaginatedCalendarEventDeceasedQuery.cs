using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetPaginated
{
    public class GetPaginatedCalendarEventDeceasedQuery : IRequest<PaginatedItems<CalendarEventDeceasedDto>>
    {
        public GetPaginatedCalendarEventDeceasedQuery(int pageSize, int pageIndex, string searchString= null, Guid? calendarEventId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			CalendarEventId = calendarEventId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public Guid? CalendarEventId { get; }
    }
}
