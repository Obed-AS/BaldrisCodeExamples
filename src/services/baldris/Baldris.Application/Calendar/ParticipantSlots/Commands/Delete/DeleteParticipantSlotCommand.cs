using System;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Commands.Delete
{
    public class DeleteParticipantSlotCommand : IRequest<int>
    {
        public DeleteParticipantSlotCommand(Guid participantSlotId)
        {
            ParticipantSlotId = participantSlotId;
        }

        public Guid ParticipantSlotId { get; }
    }
}
