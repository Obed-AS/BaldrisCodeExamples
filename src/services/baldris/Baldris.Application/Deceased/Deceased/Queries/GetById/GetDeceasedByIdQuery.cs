using System;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetById
{
    public class GetDeceasedByIdQuery : IRequest<DeceasedDto>
    {
        public GetDeceasedByIdQuery(Guid deceasedId)
        {
            DeceasedId = deceasedId;
        }

        public Guid DeceasedId { get; }
    }
}
