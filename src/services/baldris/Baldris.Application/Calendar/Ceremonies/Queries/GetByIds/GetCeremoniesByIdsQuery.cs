using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetByIds
{
    public class GetCeremoniesByIdsQuery : IRequest<IEnumerable<CeremonyDto>>
    {
        public GetCeremoniesByIdsQuery(IEnumerable<Guid> ceremonyIds)
        {
            CeremonyIds = ceremonyIds;
        }

        public IEnumerable<Guid> CeremonyIds { get; }
    }
}
