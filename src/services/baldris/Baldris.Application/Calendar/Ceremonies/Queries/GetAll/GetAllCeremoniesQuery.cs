using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetAll
{
    public class GetAllCeremoniesQuery : IRequest<IEnumerable<CeremonyDto>>
    {
        
    }
}
