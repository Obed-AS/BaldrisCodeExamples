using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetByDeceasedId
{
    public class GetGrantApplicationsByDeceasedIdQuery : IRequest<IEnumerable<GrantApplicationDto>>
    {
        public GetGrantApplicationsByDeceasedIdQuery(Guid deceasedId)
        {
            DeceasedId = deceasedId;
        }

        public Guid DeceasedId { get; }
    }
}
