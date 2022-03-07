using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.Find
{
    public class FindGroomingsQuery : IRequest<IEnumerable<GroomingDto>>
    {
        public FindGroomingsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
