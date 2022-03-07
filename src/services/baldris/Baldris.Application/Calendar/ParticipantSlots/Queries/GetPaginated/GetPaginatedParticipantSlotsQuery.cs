using System;
using Baldris.Application.Common.Models;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Queries.GetPaginated
{
    public class GetPaginatedParticipantSlotsQuery : IRequest<PaginatedItems<ParticipantSlotDto>>
    {
        public GetPaginatedParticipantSlotsQuery(int pageSize, int pageIndex, string searchString= null, bool includeSoftDeleted = false, Guid? calendarEventTypeId = null)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            SearchString = searchString;
			CalendarEventTypeId = calendarEventTypeId;
        }

        public int PageSize { get; }
        public int PageIndex { get; }
        public string SearchString { get; }
		public Guid? CalendarEventTypeId { get; }
    }
}
