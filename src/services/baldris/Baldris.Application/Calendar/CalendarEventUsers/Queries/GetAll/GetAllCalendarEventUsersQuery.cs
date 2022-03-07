using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetAll
{
    public class GetAllCalendarEventUsersQuery : IRequest<IEnumerable<CalendarEventUserDto>>
    {
        
    }
}
