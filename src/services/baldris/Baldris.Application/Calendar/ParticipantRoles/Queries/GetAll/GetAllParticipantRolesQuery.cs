using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Queries.GetAll
{
    public class GetAllParticipantRolesQuery : IRequest<IEnumerable<ParticipantRoleDto>>
    {
        public GetAllParticipantRolesQuery(bool includeSoftDeleted = false)
		{
			IncludeSoftDeleted = includeSoftDeleted;
		}

		public bool IncludeSoftDeleted { get; }
    }
}
