using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetByParticipantTypeId
{
    public class GetParticipantsByParticipantTypeIdQuery : IRequest<IEnumerable<ParticipantDto>>
    {
        public GetParticipantsByParticipantTypeIdQuery(Guid participantTypeId, bool includeSoftDeleted = false)
        {
            ParticipantTypeId = participantTypeId;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public Guid ParticipantTypeId { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
