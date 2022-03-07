using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetByIds
{
    public class GetBookingsByIdsQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetBookingsByIdsQuery(IEnumerable<Guid> bookingIds)
        {
            BookingIds = bookingIds;
        }

        public IEnumerable<Guid> BookingIds { get; }
    }
}
