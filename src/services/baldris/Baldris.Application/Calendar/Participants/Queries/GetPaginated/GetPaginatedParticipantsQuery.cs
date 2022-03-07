using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetPaginated
{
    public class GetPaginatedParticipantsQuery : IRequest<PaginatedItems<ParticipantDto>>
    {
        public GetPaginatedParticipantsQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false, Guid? participantTypeId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			IncludeSoftDeleted = includeSoftDeleted;
			ParticipantTypeId = participantTypeId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public bool IncludeSoftDeleted { get; }
		public Guid? ParticipantTypeId { get; }
    }
}
