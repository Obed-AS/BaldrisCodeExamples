using System;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Commands.Delete
{
    public class DeleteParticipantCommand : IRequest<int>
    {
        public DeleteParticipantCommand(Guid participantId)
        {
            ParticipantId = participantId;
        }

        public Guid ParticipantId { get; }
    }
}
