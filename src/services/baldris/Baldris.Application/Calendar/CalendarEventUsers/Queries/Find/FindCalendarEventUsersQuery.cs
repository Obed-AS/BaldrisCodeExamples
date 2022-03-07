using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.Find
{
    public class FindCalendarEventUsersQuery : IRequest<IEnumerable<CalendarEventUserDto>>
    {
        public FindCalendarEventUsersQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
