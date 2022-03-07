using System;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetById
{
    public class GetGraveByIdQuery : IRequest<GraveDto>
    {
        public GetGraveByIdQuery(Guid graveId)
        {
            GraveId = graveId;
        }

        public Guid GraveId { get; }
    }
}
