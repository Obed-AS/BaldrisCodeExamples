using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventParticipantDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }

        public string InternalIdentifier { get; set; }
        public string Role { get; set; }

        public int SortOrder { get; set; }
        public string Remarks { get; set; }
        public bool HideInProgram { get; set; }

        public Guid CalendarEventId { get; set; }
        public string CalendarEventSubject { get; set; }

        public Guid? ParticipantRoleId { get; set; }
        public string ParticipantRoleName { get; set; }

        public Guid ParticipantId { get; set; }
        public string ParticipantName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CalendarEventParticipant, CalendarEventParticipantDto>()
                .ForMember(target => target.Role,
                    opt => opt.MapFrom(source => source.OverriddenRoleName ?? source.ParticipantRole.Name))
                .ForMember(target => target.ParticipantName,
                    opt => opt.MapFrom(source => source.Participant.Party.DisplayName));
        }
    }
}
