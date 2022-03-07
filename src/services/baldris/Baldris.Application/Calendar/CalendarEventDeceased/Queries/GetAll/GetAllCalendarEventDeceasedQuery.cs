using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetAll
{
    public class GetAllCalendarEventDeceasedQuery : IRequest<IEnumerable<CalendarEventDeceasedDto>>
    {
        
    }
}
