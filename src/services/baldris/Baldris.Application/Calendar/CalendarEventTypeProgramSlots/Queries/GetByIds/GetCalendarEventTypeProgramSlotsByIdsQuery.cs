using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Queries.GetByIds
{
    public class GetCalendarEventTypeProgramSlotsByIdsQuery : IRequest<IEnumerable<CalendarEventTypeProgramSlotDto>>
    {
        public GetCalendarEventTypeProgramSlotsByIdsQuery(IEnumerable<Guid> calendarEventTypeProgramSlotIds)
        {
            CalendarEventTypeProgramSlotIds = calendarEventTypeProgramSlotIds;
        }

        public IEnumerable<Guid> CalendarEventTypeProgramSlotIds { get; }
    }
}
