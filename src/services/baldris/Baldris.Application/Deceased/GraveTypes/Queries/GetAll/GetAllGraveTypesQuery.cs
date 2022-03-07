using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetAll
{
    public class GetAllGraveTypesQuery : IRequest<IEnumerable<GraveTypeDto>>
    {
        public GetAllGraveTypesQuery(bool includeSoftDeleted = false)
		{
			IncludeSoftDeleted = includeSoftDeleted;
		}

		public bool IncludeSoftDeleted { get; }
    }
}
