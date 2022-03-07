using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Queries.GetAll
{
    public class GetAllParticipantTypesQuery : IRequest<IEnumerable<ParticipantTypeDto>>
    {
        public GetAllParticipantTypesQuery(bool includeSoftDeleted = false)
		{
			IncludeSoftDeleted = includeSoftDeleted;
		}

		public bool IncludeSoftDeleted { get; }
    }
}
