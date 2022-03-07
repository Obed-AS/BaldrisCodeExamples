using System;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Commands.Delete
{
    public class DeleteTransportCommand : IRequest<int>
    {
        public DeleteTransportCommand(Guid transportId)
        {
            TransportId = transportId;
        }

        public Guid TransportId { get; }
    }
}
