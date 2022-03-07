using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.Find
{
    public class FindGravesQuery : IRequest<IEnumerable<GraveDto>>
    {
        public FindGravesQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
