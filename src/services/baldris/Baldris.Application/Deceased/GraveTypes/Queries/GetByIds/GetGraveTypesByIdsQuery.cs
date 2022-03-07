using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetByIds
{
    public class GetGraveTypesByIdsQuery : IRequest<IEnumerable<GraveTypeDto>>
    {
        public GetGraveTypesByIdsQuery(IEnumerable<Guid> graveTypeIds)
        {
            GraveTypeIds = graveTypeIds;
        }

        public IEnumerable<Guid> GraveTypeIds { get; }
    }
}
