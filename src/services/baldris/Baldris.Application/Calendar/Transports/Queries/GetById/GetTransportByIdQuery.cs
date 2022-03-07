using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.GetById
{
    public class GetTransportByIdQuery : IRequest<TransportDto>
    {
        public GetTransportByIdQuery(Guid transportId)
        {
            TransportId = transportId;
        }

        public Guid TransportId { get; }
    }
}
