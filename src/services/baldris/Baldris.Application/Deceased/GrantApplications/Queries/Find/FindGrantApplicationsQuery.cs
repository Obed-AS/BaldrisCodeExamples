using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.Find
{
    public class FindGrantApplicationsQuery : IRequest<IEnumerable<GrantApplicationDto>>
    {
        public FindGrantApplicationsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
