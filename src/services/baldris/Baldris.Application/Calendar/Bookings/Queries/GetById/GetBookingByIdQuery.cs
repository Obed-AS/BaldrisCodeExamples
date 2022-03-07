using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetById
{
    public class GetBookingByIdQuery : IRequest<BookingDto>
    {
        public GetBookingByIdQuery(Guid bookingId)
        {
            BookingId = bookingId;
        }

        public Guid BookingId { get; }
    }
}
