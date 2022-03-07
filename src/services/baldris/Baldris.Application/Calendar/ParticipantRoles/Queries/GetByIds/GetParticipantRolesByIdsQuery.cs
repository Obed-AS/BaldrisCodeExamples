using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Queries.GetByIds
{
    public class GetParticipantRolesByIdsQuery : IRequest<IEnumerable<ParticipantRoleDto>>
    {
        public GetParticipantRolesByIdsQuery(IEnumerable<Guid> participantRoleIds)
        {
            ParticipantRoleIds = participantRoleIds;
        }

        public IEnumerable<Guid> ParticipantRoleIds { get; }
    }
}
