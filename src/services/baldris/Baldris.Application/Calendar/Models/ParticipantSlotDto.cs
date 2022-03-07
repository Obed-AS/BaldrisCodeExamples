using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class ParticipantSlotDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }

        public Guid ParticipantRoleId { get; set; }
        public string ParticipantRoleName { get; set; }

        public Guid? ParticipantTypeId { get; set; }
        public string ParticipantTypeName { get; set; }

        public Guid CalendarEventTypeId { get; set; }
        public string CalendarEventTypeName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ParticipantSlot, ParticipantSlotDto>();
        }
    }
}
