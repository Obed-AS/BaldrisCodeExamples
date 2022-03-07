using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Queries.GetAll
{
    public class GetAllTransportsQuery : IRequest<IEnumerable<TransportDto>>
    {
        
    }
}
