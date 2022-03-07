using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.Find
{
    public class FindCalendarEventTypeProgramSlotsQuery : IRequest<IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        public FindCalendarEventTypeProgramSlotsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
