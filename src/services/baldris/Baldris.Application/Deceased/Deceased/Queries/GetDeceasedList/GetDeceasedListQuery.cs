using System;
using System.Collections.Generic;
using Baldris.Application.Deceased.Models;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Queries.GetDeceasedList {
    public class GetDeceasedListQuery : IRequest<IList<DeceasedListItemDto>> {
        public DateTime? DateOfDeathStartDate { get; set; }
        public DateTime? DateOfDeathEndDate { get; set; }
        public string PlaceOfDeathSearchString { get; set; }
        public string PlaceOfBirthSearchString { get; set; }
    }
}