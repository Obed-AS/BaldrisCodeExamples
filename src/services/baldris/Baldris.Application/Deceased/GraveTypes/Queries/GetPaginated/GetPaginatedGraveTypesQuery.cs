using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetPaginated
{
    public class GetPaginatedGraveTypesQuery : IRequest<PaginatedItems<GraveTypeDto>>
    {
        public GetPaginatedGraveTypesQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false)
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
