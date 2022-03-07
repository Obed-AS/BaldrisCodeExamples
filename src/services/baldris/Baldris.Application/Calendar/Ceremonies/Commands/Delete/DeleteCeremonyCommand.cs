using System;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Commands.Delete
{
    public class DeleteCeremonyCommand : IRequest<int>
    {
        public DeleteCeremonyCommand(Guid ceremonyId)
        {
            CeremonyId = ceremonyId;
        }

        public Guid CeremonyId { get; }
    }
}
