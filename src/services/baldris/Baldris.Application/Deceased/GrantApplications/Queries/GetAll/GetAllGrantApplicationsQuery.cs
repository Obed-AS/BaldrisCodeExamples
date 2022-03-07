using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.GrantApplications.Queries.GetAll
{
    public class GetAllGrantApplicationsQuery : IRequest<IEnumerable<GrantApplicationDto>>
    {
        
    }
}
