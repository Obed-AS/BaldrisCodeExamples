using System;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Commands.Delete
{
    public class DeleteDeceasedCommand : IRequest<int>
    {
        public DeleteDeceasedCommand(Guid deceasedId)
        {
            DeceasedId = deceasedId;
        }

        public Guid DeceasedId { get; }
    }
}
