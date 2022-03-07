using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.ParticipantSlots.Commands.Create;
using Baldris.Application.Calendar.ParticipantSlots.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantSlots.Commands.UpdateOrInsert
{
    public class UpdateOrInsertParticipantSlotCommand : IRequest<ParticipantSlotDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Remarks { get; set; }

        public Guid ParticipantRoleId { get; set; }

        public Guid? ParticipantTypeId { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertParticipantSlotCommand, ParticipantSlot>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertParticipantSlotCommand, UpdateParticipantSlotCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertParticipantSlotCommand, CreateParticipantSlotCommand>().ReverseMap();
        }
    }
}
