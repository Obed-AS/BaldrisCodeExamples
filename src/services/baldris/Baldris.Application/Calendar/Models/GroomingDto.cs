using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class GroomingDto : CalendarEventDto, IHaveCustomMapping
    {
        public string ObjectType => typeof(GroomingDto).ToString();
        public Guid? ClothingId { get; set; }
        public string ClothingLookupValue { get; set; }
        public string ClothingSerialized { get; set; }

        public string PersonalClothing { get; set; }
        public string PersonalBelongings { get; set; }
        public string GroomingDescription { get; set; }
        
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Grooming, GroomingDto>()
                .IncludeBase<CalendarEvent, CalendarEventDto>();
        }
    }
}
