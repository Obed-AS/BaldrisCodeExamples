using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetByCalendarEventTypeId
{
    public class GetBookingsByCalendarEventTypeIdQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetBookingsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
