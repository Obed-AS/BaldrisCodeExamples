using System;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Commands.Delete
{
    public class DeleteParticipantTypeCommand : IRequest<int>
    {
        public DeleteParticipantTypeCommand(Guid participantTypeId)
        {
            ParticipantTypeId = participantTypeId;
        }

        public Guid ParticipantTypeId { get; }
    }
}
