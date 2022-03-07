using System;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Commands.Delete
{
    public class DeleteGroomingCommand : IRequest<int>
    {
        public DeleteGroomingCommand(Guid groomingId)
        {
            GroomingId = groomingId;
        }

        public Guid GroomingId { get; }
    }
}
