using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventDeceasedDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public OpenCasket OpenCasket { get; set; }

        public Guid CalendarEventId { get; set; }
        public string CalendarEventSubject { get; set; }

        public Guid DeceasedId { get; set; }
        public string DeceasedName { get; set; }
        public DateTime? DeceasedDateOfBirth { get; set; }
        public DateTime? DeceasedDateOfDeath { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Common.CalendarEventDeceased, CalendarEventDeceasedDto>()
                .ForMember(target => target.DeceasedName,
                    opt => opt.MapFrom(source => source.Deceased.Person.DisplayName))
                .ForMember(target => target.DeceasedDateOfBirth,
                    opt => opt.MapFrom(source => source.Deceased.Person.DateOfBirth))
                .ForMember(target => target.DeceasedDateOfDeath,
                    opt => opt.MapFrom(source => source.Deceased.Person.DateOfDeath));
        }
    }
}
