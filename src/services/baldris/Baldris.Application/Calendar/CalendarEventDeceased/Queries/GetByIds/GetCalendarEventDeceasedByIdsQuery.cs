using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Queries.GetByIds
{
    public class GetCalendarEventDeceasedByIdsQuery : IRequest<IEnumerable<CalendarEventDeceasedDto>>
    {
        public GetCalendarEventDeceasedByIdsQuery(IEnumerable<Guid> calendarEventDeceasedIds)
        {
            CalendarEventDeceasedIds = calendarEventDeceasedIds;
        }

        public IEnumerable<Guid> CalendarEventDeceasedIds { get; }
    }
}
