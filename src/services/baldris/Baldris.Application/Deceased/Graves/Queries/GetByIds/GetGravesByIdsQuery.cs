using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetByIds
{
    public class GetGravesByIdsQuery : IRequest<IEnumerable<GraveDto>>
    {
        public GetGravesByIdsQuery(IEnumerable<Guid> graveIds)
        {
            GraveIds = graveIds;
        }

        public IEnumerable<Guid> GraveIds { get; }
    }
}
