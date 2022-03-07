using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Queries.GetPaginated
{
    public class GetPaginatedCalendarEventTypesQuery : IRequest<PaginatedItems<CalendarEventTypeDto>>
    {
        public GetPaginatedCalendarEventTypesQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
