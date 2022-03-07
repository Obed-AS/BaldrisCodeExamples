using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetPaginated
{
    public class GetPaginatedCalendarEventUsersQuery : IRequest<PaginatedItems<CalendarEventUserDto>>
    {
        public GetPaginatedCalendarEventUsersQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false, Guid? calendarEventId = null)
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
