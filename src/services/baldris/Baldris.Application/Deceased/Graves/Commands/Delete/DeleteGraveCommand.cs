using System;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Commands.Delete
{
    public class DeleteGraveCommand : IRequest<int>
    {
        public DeleteGraveCommand(Guid graveId, Guid? targetWorkOrderId = null)
        {
            GraveId = graveId;
            TargetWorkOrderId = targetWorkOrderId;
        }

        public Guid GraveId { get; }
        public Guid? TargetWorkOrderId { get; }
    }
}
