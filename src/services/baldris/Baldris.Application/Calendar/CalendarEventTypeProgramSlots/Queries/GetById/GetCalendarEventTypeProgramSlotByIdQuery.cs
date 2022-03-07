using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetById
{
    public class GetCalendarEventTypeProgramSlotByIdQuery : IRequest<CalendarEventTypeProgramSlotDto>
    {
        public GetCalendarEventTypeProgramSlotByIdQuery(Guid calendarEventTypeProgramSlotId)
        {
            CalendarEventTypeProgramSlotId = calendarEventTypeProgramSlotId;
        }

        public Guid CalendarEventTypeProgramSlotId { get; }
    }
}
