using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.GetAll
{
    public class GetAllCalendarEventEquipmentQuery : IRequest<IEnumerable<CalendarEventEquipmentDto>>
    {
        
    }
}
