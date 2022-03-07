using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.Find
{
    public class FindDeceasedQuery : IRequest<IEnumerable<DeceasedDto>>
    {
        public FindDeceasedQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
