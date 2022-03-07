using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Queries.GetAll
{
    public class GetAllCalendarEventParticipantsQuery : IRequest<IEnumerable<CalendarEventParticipantDto>>
    {
        
    }
}
