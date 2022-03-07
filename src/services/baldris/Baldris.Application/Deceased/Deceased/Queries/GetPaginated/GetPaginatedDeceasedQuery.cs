using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetPaginated
{
    public class GetPaginatedDeceasedQuery : IRequest<PaginatedItems<DeceasedDto>>
    {
        public GetPaginatedDeceasedQuery(int pageSize, int pageIndex, string searchString= null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
    }
}
