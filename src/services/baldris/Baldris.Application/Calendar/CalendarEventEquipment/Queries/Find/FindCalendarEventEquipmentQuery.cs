using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Queries.Find
{
    public class FindCalendarEventEquipmentQuery : IRequest<IEnumerable<CalendarEventEquipmentDto>>
    {
        public FindCalendarEventEquipmentQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
