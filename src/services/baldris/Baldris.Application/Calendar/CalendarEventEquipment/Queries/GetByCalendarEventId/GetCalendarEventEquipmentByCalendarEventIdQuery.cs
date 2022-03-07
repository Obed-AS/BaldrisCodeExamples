using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetByCalendarEventId
{
    public class GetCalendarEventEquipmentByCalendarEventIdQuery : IRequest<IEnumerable<CalendarEventEquipmentDto>>
    {
        public GetCalendarEventEquipmentByCalendarEventIdQuery(Guid calendarEventId)
        {
            CalendarEventId = calendarEventId;
        }

        public Guid CalendarEventId { get; }
    }
}
