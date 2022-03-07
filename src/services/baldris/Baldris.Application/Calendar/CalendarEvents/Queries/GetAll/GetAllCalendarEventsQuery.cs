using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetAll
{
    public class GetAllCalendarEventsQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        
    }
}
