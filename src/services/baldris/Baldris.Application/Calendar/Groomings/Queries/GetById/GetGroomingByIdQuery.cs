using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.GetById
{
    public class GetGroomingByIdQuery : IRequest<GroomingDto>
    {
        public GetGroomingByIdQuery(Guid groomingId)
        {
            GroomingId = groomingId;
        }

        public Guid GroomingId { get; }
    }
}
