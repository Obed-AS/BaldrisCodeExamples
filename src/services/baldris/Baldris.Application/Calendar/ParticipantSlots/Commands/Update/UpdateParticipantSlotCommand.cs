using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Commands.Update
{
    public class UpdateParticipantSlotCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }

        public Guid ParticipantRoleId { get; set; }

        public Guid? ParticipantTypeId { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateParticipantSlotCommand, ParticipantSlot>();
        }
    }
}
