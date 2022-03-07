using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetAll
{
    public class GetAllDeceasedQuery : IRequest<IEnumerable<DeceasedDto>>
    {
        
    }
}
