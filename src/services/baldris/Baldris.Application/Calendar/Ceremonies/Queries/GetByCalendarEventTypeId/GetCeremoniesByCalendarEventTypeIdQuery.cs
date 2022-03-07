using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetByCalendarEventTypeId
{
    public class GetCeremoniesByCalendarEventTypeIdQuery : IRequest<IEnumerable<CeremonyDto>>
    {
        public GetCeremoniesByCalendarEventTypeIdQuery(Guid calendarEventTypeId)
        {
            CalendarEventTypeId = calendarEventTypeId;
        }

        public Guid CalendarEventTypeId { get; }
    }
}
