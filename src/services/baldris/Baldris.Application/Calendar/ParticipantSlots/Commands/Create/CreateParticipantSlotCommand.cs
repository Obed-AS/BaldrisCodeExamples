using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Commands.Create
{
    public class CreateParticipantSlotCommand : IRequest<ParticipantSlotDto>, IHaveCustomMapping
    {
        public string Remarks { get; set; }

        public Guid ParticipantRoleId { get; set; }

        public Guid? ParticipantTypeId { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateParticipantSlotCommand, ParticipantSlot>();

        }
    }
}
