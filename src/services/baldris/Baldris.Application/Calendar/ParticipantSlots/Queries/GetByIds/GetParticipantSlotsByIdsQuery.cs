using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetByIds
{
    public class GetParticipantSlotsByIdsQuery : IRequest<IEnumerable<ParticipantSlotDto>>
    {
        public GetParticipantSlotsByIdsQuery(IEnumerable<Guid> participantSlotIds)
        {
            ParticipantSlotIds = participantSlotIds;
        }

        public IEnumerable<Guid> ParticipantSlotIds { get; }
    }
}
