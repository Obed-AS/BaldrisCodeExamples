using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.Create;
using Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventTypeProgramSlotCommand : IRequest<CalendarEventTypeProgramSlotDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public Guid ProgramSlotId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeProgramSlotCommand, CalendarEventTypeProgramSlot>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeProgramSlotCommand, UpdateCalendarEventTypeProgramSlotCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeProgramSlotCommand, CreateCalendarEventTypeProgramSlotCommand>().ReverseMap();
        }
    }
}
