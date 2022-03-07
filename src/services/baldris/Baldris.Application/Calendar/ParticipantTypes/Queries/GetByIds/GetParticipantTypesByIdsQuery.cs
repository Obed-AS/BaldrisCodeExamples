using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Queries.GetByIds
{
    public class GetParticipantTypesByIdsQuery : IRequest<IEnumerable<ParticipantTypeDto>>
    {
        public GetParticipantTypesByIdsQuery(IEnumerable<Guid> participantTypeIds)
        {
            ParticipantTypeIds = participantTypeIds;
        }

        public IEnumerable<Guid> ParticipantTypeIds { get; }
    }
}
