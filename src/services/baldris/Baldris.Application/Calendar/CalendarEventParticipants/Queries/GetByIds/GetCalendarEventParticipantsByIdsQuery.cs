using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.GetByIds
{
    public class GetCalendarEventParticipantsByIdsQuery : IRequest<IEnumerable<CalendarEventParticipantDto>>
    {
        public GetCalendarEventParticipantsByIdsQuery(IEnumerable<Guid> calendarEventParticipantIds)
        {
            CalendarEventParticipantIds = calendarEventParticipantIds;
        }

        public IEnumerable<Guid> CalendarEventParticipantIds { get; }
    }
}
