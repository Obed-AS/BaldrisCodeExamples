using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetAll
{
    public class GetAllBookingsQuery : IRequest<IEnumerable<BookingDto>>
    {
        
    }
}
