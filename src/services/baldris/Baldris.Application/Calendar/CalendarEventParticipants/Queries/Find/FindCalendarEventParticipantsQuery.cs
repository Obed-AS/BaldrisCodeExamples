using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.Find
{
    public class FindCalendarEventParticipantsQuery : IRequest<IEnumerable<CalendarEventParticipantDto>>
    {
        public FindCalendarEventParticipantsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
