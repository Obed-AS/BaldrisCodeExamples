using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Queries.Find
{
    public class FindParticipantRolesQuery : IRequest<IEnumerable<ParticipantRoleDto>>
    {
        public FindParticipantRolesQuery(string searchString, bool includeSoftDeleted = false)
        {
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
