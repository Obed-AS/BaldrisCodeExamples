using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetPaginated
{
    public class GetPaginatedBookingsQuery : IRequest<PaginatedItems<BookingDto>>
    {
        public GetPaginatedBookingsQuery(int pageSize, int pageIndex, string searchString= null, Guid? calendarEventTypeId = null)
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
