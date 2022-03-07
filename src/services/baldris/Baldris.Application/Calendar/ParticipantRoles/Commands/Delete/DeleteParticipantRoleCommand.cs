using System;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Commands.Delete
{
    public class DeleteParticipantRoleCommand : IRequest<int>
    {
        public DeleteParticipantRoleCommand(Guid participantRoleId)
        {
            ParticipantRoleId = participantRoleId;
        }

        public Guid ParticipantRoleId { get; }
    }
}
