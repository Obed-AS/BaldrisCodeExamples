using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetByIds
{
    public class GetParticipantsByIdsQuery : IRequest<IEnumerable<ParticipantDto>>
    {
        public GetParticipantsByIdsQuery(IEnumerable<Guid> participantIds)
        {
            ParticipantIds = participantIds;
        }

        public IEnumerable<Guid> ParticipantIds { get; }
    }
}
