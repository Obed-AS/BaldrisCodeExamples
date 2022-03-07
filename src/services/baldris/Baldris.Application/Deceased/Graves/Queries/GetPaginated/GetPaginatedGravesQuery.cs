using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetPaginated
{
    public class GetPaginatedGravesQuery : IRequest<PaginatedItems<GraveDto>>
    {
        public GetPaginatedGravesQuery(int pageSize, int pageIndex, string searchString= null, Guid? graveTypeId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			GraveTypeId = graveTypeId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public Guid? GraveTypeId { get; }
    }
}
