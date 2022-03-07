using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.Find
{
    public class FindParticipantSlotsQuery : IRequest<IEnumerable<ParticipantSlotDto>>
    {
        public FindParticipantSlotsQuery(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; }
    }
}
