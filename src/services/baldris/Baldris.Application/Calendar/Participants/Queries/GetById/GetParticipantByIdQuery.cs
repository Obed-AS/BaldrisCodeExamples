using System;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetById
{
    public class GetParticipantByIdQuery : IRequest<ParticipantDto>
    {
        public GetParticipantByIdQuery(Guid participantId)
        {
            ParticipantId = participantId;
        }

        public Guid ParticipantId { get; }
    }
}
