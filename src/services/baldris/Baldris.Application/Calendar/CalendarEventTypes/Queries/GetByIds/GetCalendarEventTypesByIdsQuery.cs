using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Queries.GetByIds
{
    public class GetCalendarEventTypesByIdsQuery : IRequest<IEnumerable<CalendarEventTypeDto>>
    {
        public GetCalendarEventTypesByIdsQuery(IEnumerable<Guid> calendarEventTypeIds)
        {
            CalendarEventTypeIds = calendarEventTypeIds;
        }

        public IEnumerable<Guid> CalendarEventTypeIds { get; }
    }
}
