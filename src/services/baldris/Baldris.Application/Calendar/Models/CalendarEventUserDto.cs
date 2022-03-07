using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventUserDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; }
        public string Role { get; set; }
        public string Remarks { get; set; }
        public RsvpStatus RsvpStatus { get; set; }
        public DateTime? RsvpChangedTime { get; set; }
        public string RsvpMessage { get; set; }

        public Guid CalendarEventId { get; set; }
        public string CalendarEventSubject { get; set; }

        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string UserEmail { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CalendarEventUser, CalendarEventUserDto>()
                .ForMember(target => target.UserProfileImageUrl,
                    opt => opt.MapFrom(source => source.User.ProfileImage.Filepath));
        }
    }
}
