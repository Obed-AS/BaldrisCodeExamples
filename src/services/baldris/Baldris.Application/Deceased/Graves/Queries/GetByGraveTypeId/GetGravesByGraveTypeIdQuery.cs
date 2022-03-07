using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetByGraveTypeId
{
    public class GetGravesByGraveTypeIdQuery : IRequest<IEnumerable<GraveDto>>
    {
        public GetGravesByGraveTypeIdQuery(Guid graveTypeId)
        {
            GraveTypeId = graveTypeId;
        }

        public Guid GraveTypeId { get; }
    }
}
