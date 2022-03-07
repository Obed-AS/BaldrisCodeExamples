using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Queries.GetAll
{
    public class GetAllGroomingsQuery : IRequest<IEnumerable<GroomingDto>>
    {
        
    }
}
