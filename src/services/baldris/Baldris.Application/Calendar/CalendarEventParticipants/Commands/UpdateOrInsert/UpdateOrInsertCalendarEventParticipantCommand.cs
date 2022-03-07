using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventParticipants.Commands.Create;
using Baldris.Application.Calendar.CalendarEventParticipants.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventParticipantCommand : IRequest<CalendarEventParticipantDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string InternalIdentifier { get; set; }
        public string OverriddenRoleName { get; set; }

        public int SortOrder { get; set; }
        public string Remarks { get; set; }
        public bool HideInProgram { get; set; }

        public Guid CalendarEventId { get; set; }

        public Guid? ParticipantRoleId { get; set; }

        public Guid ParticipantId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventParticipantCommand, CalendarEventParticipant>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventParticipantCommand, UpdateCalendarEventParticipantCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventParticipantCommand, CreateCalendarEventParticipantCommand>().ReverseMap();
        }
    }
}
