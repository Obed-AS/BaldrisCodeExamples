using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.Find
{
    public class FindCeremoniesQuery : IRequest<IEnumerable<CeremonyDto>>
    {
        public FindCeremoniesQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
