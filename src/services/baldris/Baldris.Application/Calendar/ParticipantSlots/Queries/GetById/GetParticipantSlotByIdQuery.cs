using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetById
{
    public class GetParticipantSlotByIdQuery : IRequest<ParticipantSlotDto>
    {
        public GetParticipantSlotByIdQuery(Guid participantSlotId)
        {
            ParticipantSlotId = participantSlotId;
        }

        public Guid ParticipantSlotId { get; }
    }
}
