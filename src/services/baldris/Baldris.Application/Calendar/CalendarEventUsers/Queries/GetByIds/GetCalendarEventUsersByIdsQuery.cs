using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Queries.GetByIds
{
    public class GetCalendarEventUsersByIdsQuery : IRequest<IEnumerable<CalendarEventUserDto>>
    {
        public GetCalendarEventUsersByIdsQuery(IEnumerable<Guid> calendarEventUserIds)
        {
            CalendarEventUserIds = calendarEventUserIds;
        }

        public IEnumerable<Guid> CalendarEventUserIds { get; }
    }
}
