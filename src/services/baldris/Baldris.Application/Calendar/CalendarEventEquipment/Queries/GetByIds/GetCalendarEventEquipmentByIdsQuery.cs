using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetByIds
{
    public class GetCalendarEventEquipmentByIdsQuery : IRequest<IEnumerable<CalendarEventEquipmentDto>>
    {
        public GetCalendarEventEquipmentByIdsQuery(IEnumerable<Guid> calendarEventEquipmentIds)
        {
            CalendarEventEquipmentIds = calendarEventEquipmentIds;
        }

        public IEnumerable<Guid> CalendarEventEquipmentIds { get; }
    }
}
