using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetAll
{
    public class GetAllParticipantsQuery : IRequest<IEnumerable<ParticipantDto>>
    {
        public GetAllParticipantsQuery(bool includeSoftDeleted = false)
		{
			IncludeSoftDeleted = includeSoftDeleted;
		}

		public bool IncludeSoftDeleted { get; }
    }
}
