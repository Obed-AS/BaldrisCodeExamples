using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventParticipants.Commands.Update
{
    public class UpdateCalendarEventParticipantCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
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
            configuration.CreateMap<UpdateCalendarEventParticipantCommand, CalendarEventParticipant>();
        }
    }
}
