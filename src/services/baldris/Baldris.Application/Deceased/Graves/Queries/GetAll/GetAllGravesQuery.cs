using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Queries.GetAll
{
    public class GetAllGravesQuery : IRequest<IEnumerable<GraveDto>>
    {
        
    }
}
