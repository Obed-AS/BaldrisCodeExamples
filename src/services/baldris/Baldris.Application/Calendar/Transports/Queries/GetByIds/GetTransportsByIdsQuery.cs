using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.GetByIds
{
    public class GetTransportsByIdsQuery : IRequest<IEnumerable<TransportDto>>
    {
        public GetTransportsByIdsQuery(IEnumerable<Guid> transportIds)
        {
            TransportIds = transportIds;
        }

        public IEnumerable<Guid> TransportIds { get; }
    }
}
