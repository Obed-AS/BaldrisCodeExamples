using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetAll
{
    public class GetAllParticipantSlotsQuery : IRequest<IEnumerable<ParticipantSlotDto>>
    {
        
    }
}
