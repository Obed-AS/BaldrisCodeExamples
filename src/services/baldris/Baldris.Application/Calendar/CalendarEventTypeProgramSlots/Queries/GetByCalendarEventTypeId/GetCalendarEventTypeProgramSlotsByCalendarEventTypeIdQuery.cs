using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetByCalendarEventTypeId
{
    public class GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdQuery : IRequest<IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        public GetCalendarEventTypeProgramSlotsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
