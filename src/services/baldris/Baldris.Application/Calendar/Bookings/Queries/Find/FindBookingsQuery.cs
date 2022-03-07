using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.Find
{
    public class FindBookingsQuery : IRequest<IEnumerable<BookingDto>>
    {
        public FindBookingsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
