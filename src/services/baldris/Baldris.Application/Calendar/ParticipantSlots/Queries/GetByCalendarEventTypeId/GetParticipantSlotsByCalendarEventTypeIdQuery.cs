using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetByCalendarEventTypeId
{
    public class GetParticipantSlotsByCalendarEventTypeIdQuery : IRequest<IEnumerable<ParticipantSlotDto>>
    {
        public GetParticipantSlotsByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
