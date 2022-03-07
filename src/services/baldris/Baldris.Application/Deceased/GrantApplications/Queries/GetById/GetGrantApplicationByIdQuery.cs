using System;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetById
{
    public class GetGrantApplicationByIdQuery : IRequest<GrantApplicationDto>
    {
        public GetGrantApplicationByIdQuery(Guid grantApplicationId)
        {
            GrantApplicationId = grantApplicationId;
        }

        public Guid GrantApplicationId { get; }
    }
}
