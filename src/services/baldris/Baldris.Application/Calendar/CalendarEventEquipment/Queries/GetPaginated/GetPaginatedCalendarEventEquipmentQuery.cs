using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetPaginated
{
    public class GetPaginatedCalendarEventEquipmentQuery : IRequest<PaginatedItems<CalendarEventEquipmentDto>>
    {
        public GetPaginatedCalendarEventEquipmentQuery(int pageSize, int pageIndex, string searchString= null, Guid? calendarEventId = null)
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
