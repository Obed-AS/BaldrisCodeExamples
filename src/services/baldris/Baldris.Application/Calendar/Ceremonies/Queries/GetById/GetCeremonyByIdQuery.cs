using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Queries.GetById
{
    public class GetCeremonyByIdQuery : IRequest<CeremonyDto>
    {
        public GetCeremonyByIdQuery(Guid ceremonyId)
        {
            CeremonyId = ceremonyId;
        }

        public Guid CeremonyId { get; }
    }
}
