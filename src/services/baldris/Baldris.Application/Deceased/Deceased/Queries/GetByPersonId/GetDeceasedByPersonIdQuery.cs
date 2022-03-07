using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetByPersonId
{
    public class GetDeceasedByPersonIdQuery : IRequest<IEnumerable<DeceasedDto>>
    {
        public GetDeceasedByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }

        public Guid PersonId { get; } 
    }
}
