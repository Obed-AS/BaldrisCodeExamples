using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetPaginated
{
    public class GetPaginatedGrantApplicationsQuery : IRequest<PaginatedItems<GrantApplicationDto>>
    {
        public GetPaginatedGrantApplicationsQuery(int pageSize, int pageIndex, string searchString= null, Guid? deceasedId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			DeceasedId = deceasedId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public Guid? DeceasedId { get; }
    }
}
