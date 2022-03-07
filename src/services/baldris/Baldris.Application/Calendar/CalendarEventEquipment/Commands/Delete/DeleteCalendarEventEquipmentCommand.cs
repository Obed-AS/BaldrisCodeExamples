using System;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Commands.Delete
{
    public class DeleteCalendarEventEquipmentCommand : IRequest<int>
    {
        public DeleteCalendarEventEquipmentCommand(Guid calendarEventEquipmentId)
        {
            CalendarEventEquipmentId = calendarEventEquipmentId;
        }

        public Guid CalendarEventEquipmentId { get; }
    }
}
