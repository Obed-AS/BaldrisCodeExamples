using System;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Queries.GetById
{
    public class GetGraveTypeByIdQuery : IRequest<GraveTypeDto>
    {
        public GetGraveTypeByIdQuery(Guid graveTypeId)
        {
            GraveTypeId = graveTypeId;
        }

        public Guid GraveTypeId { get; }
    }
}
