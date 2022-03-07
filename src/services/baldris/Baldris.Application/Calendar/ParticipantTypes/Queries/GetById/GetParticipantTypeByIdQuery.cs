using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Queries.GetById
{
    public class GetParticipantTypeByIdQuery : IRequest<ParticipantTypeDto>
    {
        public GetParticipantTypeByIdQuery(Guid participantTypeId)
        {
            ParticipantTypeId = participantTypeId;
        }

        public Guid ParticipantTypeId { get; }
    }
}
