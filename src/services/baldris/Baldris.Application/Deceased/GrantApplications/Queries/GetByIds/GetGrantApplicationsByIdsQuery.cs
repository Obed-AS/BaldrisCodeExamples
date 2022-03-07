using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetByIds
{
    public class GetGrantApplicationsByIdsQuery : IRequest<IEnumerable<GrantApplicationDto>>
    {
        public GetGrantApplicationsByIdsQuery(IEnumerable<Guid> grantApplicationIds)
        {
            GrantApplicationIds = grantApplicationIds;
        }

        public IEnumerable<Guid> GrantApplicationIds { get; }
    }
}
