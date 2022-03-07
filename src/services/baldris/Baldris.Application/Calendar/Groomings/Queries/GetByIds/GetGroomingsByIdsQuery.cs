using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.GetByIds
{
    public class GetGroomingsByIdsQuery : IRequest<IEnumerable<GroomingDto>>
    {
        public GetGroomingsByIdsQuery(IEnumerable<Guid> groomingIds)
        {
            GroomingIds = groomingIds;
        }

        public IEnumerable<Guid> GroomingIds { get; }
    }
}
