using System;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Commands.Delete
{
    public class DeleteBookingCommand : IRequest<int>
    {
        public DeleteBookingCommand(Guid bookingId)
        {
            BookingId = bookingId;
        }

        public Guid BookingId { get; }
    }
}
