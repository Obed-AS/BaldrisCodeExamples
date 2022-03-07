using System;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Commands.Delete
{
    public class DeleteGrantApplicationCommand : IRequest<int>
    {
        public DeleteGrantApplicationCommand(Guid grantApplicationId)
        {
            GrantApplicationId = grantApplicationId;
        }

        public Guid GrantApplicationId { get; }
    }
}
