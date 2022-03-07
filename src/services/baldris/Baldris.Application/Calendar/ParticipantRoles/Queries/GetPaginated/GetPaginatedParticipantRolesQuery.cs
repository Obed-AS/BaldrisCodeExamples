using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Queries.GetPaginated
{
    public class GetPaginatedParticipantRolesQuery : IRequest<PaginatedItems<ParticipantRoleDto>>
    {
        public GetPaginatedParticipantRolesQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
