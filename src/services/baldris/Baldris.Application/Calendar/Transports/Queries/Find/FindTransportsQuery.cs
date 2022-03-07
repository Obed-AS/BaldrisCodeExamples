using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.Find
{
    public class FindTransportsQuery : IRequest<IEnumerable<TransportDto>>
    {
        public FindTransportsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
