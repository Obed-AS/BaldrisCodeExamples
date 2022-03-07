using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Queries.Find
{
    public class FindParticipantTypesQuery : IRequest<IEnumerable<ParticipantTypeDto>>
    {
        public FindParticipantTypesQuery(string searchString, bool includeSoftDeleted = false)
        {
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
