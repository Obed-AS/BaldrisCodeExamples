using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventListItemDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public CalendarEventOptions Options { get; set; }
        
        public bool IsAllDay { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string LocationImageUrl { get; set; }
        public string Address { get; set; }

        public string CalendarEventType { get; set; }
        public string CalendarEventTypeIcon { get; set; }
        public string CalendarEventTypeColor { get; set; }

        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<CalendarEvent, CalendarEventListItemDto>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(target => target.StartTime, opt => opt.MapFrom(source => source.Start))
                .ForMember(target => target.EndTime, opt => opt.MapFrom(source => source.End))
                .ForMember(target => target.Options, opt => opt.MapFrom(source => source.Options))
                .ForMember(target => target.IsAllDay, opt => opt.MapFrom(source => source.IsAllDay))
                .ForMember(target => target.Location, opt => opt.MapFrom(source => source.Location.Name))
                .ForMember(target => target.LocationImageUrl, opt => opt.MapFrom(source => source.Location.Image.Filepath))
                .ForMember(target => target.Address, opt => opt.MapFrom(source => source.Address.LocalizedAddress))
                .ForMember(target => target.CalendarEventType,
                    opt => opt.MapFrom(source => source.CalendarEventType.Name))
                .ForMember(target => target.CalendarEventTypeIcon,
                    opt => opt.MapFrom(source => source.CalendarEventType.IconName))
                .ForMember(target => target.CalendarEventTypeColor,
                    opt => opt.MapFrom(source => source.CalendarEventType.Color));
        }
    }
}