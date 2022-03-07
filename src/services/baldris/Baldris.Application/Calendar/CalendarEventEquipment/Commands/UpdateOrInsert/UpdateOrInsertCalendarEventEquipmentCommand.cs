using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventEquipment.Commands.Create;
using Baldris.Application.Calendar.CalendarEventEquipment.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventEquipmentCommand : IRequest<CalendarEventEquipmentDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid EquipmentId { get; set; }

        public Guid CalendarEventId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventEquipmentCommand, Entities.Common.CalendarEventEquipment>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventEquipmentCommand, UpdateCalendarEventEquipmentCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventEquipmentCommand, CreateCalendarEventEquipmentCommand>().ReverseMap();
        }
    }
}
