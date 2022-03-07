using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventEquipmentDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentImageUrl { get; set; }

        public Guid CalendarEventId { get; set; }
        public string CalendarEventSubject { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Common.CalendarEventEquipment, CalendarEventEquipmentDto>()
                .ForMember(target => target.EquipmentName,
                    opt => opt.MapFrom(source => source.Equipment.Name))
                .ForMember(target => target.EquipmentType,
                    opt => opt.MapFrom(source => source.Equipment.EquipmentType.Name))
                .ForMember(target => target.EquipmentImageUrl,
                    opt => opt.MapFrom(source => source.Equipment.Image.Filepath));
        }
    }
}
