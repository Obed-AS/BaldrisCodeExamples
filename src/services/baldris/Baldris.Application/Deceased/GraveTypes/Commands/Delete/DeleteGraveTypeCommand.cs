using System;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Commands.Delete
{
    public class DeleteGraveTypeCommand : IRequest<int>
    {
        public DeleteGraveTypeCommand(Guid graveTypeId)
        {
            GraveTypeId = graveTypeId;
        }

        public Guid GraveTypeId { get; }
    }
}
