using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetByIds
{
    public class GetDeceasedByIdsQuery : IRequest<IEnumerable<DeceasedDto>>
    {
        public GetDeceasedByIdsQuery(IEnumerable<Guid> deceasedIds)
        {
            DeceasedIds = deceasedIds;
        }

        public IEnumerable<Guid> DeceasedIds { get; }
    }
}
