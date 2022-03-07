using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Queries.Find
{
    public class FindGraveTypesQuery : IRequest<IEnumerable<GraveTypeDto>>
    {
        public FindGraveTypesQuery(string searchString, bool includeSoftDeleted = false)
        {
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
        }

        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
    }
}
