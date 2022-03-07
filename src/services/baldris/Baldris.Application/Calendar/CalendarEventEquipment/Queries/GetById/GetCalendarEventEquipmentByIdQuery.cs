using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetById
{
    public class GetCalendarEventEquipmentByIdQuery : IRequest<CalendarEventEquipmentDto>
    {
        public GetCalendarEventEquipmentByIdQuery(Guid calendarEventEquipmentId)
        {
            CalendarEventEquipmentId = calendarEventEquipmentId;
        }

        public Guid CalendarEventEquipmentId { get; }
    }
}
