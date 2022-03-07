using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetAll
{
    public class GetAllCalendarEventTypeProgramSlotsQuery : IRequest<IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        
    }
}
