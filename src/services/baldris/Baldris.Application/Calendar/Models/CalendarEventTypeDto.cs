using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventTypeDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string Color { get; set; }
        public bool IsRequiredBySystem { get; set; }
        public bool IsInternal { get; set; }
        public bool IsDeleted { get; set; }
        public string IconName { get; set; }
        public CalendarEventOptions Options { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramTitleAlternative { get; set; }
        public string ProgramTitleEnglish { get; set; }

        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }

        public Guid? DefaultSupplierId { get; set; }
        public string DefaultSupplierName { get; set; }
        public string DefaultSupplierConfig { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CalendarEventType, CalendarEventTypeDto>()
                .ForMember(target => target.ImageUrl,
                    opt => opt.MapFrom(source => source.Image.Filepath))
                .ForMember(target => target.DefaultSupplierName,
                    opt => opt.MapFrom(source => source.DefaultSupplier.Party.DisplayName));
        }
    }
}
