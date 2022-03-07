using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.Find
{
    public class FindParticipantsQuery : IRequest<IEnumerable<ParticipantDto>>
    {
        public FindParticipantsQuery(string searchString, bool includeSoftDeleted = false)
        {
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
