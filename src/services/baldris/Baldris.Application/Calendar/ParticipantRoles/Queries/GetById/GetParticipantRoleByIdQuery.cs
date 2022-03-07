using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Queries.GetById
{
    public class GetParticipantRoleByIdQuery : IRequest<ParticipantRoleDto>
    {
        public GetParticipantRoleByIdQuery(Guid participantRoleId)
        {
            ParticipantRoleId = participantRoleId;
        }

        public Guid ParticipantRoleId { get; }
    }
}
