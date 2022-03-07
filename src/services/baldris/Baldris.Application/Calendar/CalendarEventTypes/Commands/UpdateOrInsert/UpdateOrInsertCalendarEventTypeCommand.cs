using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventTypes.Commands.Create;
using Baldris.Application.Calendar.CalendarEventTypes.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypes.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventTypeCommand : IRequest<CalendarEventTypeDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
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

        public Guid? DefaultSupplierId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeCommand, CalendarEventType>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeCommand, UpdateCalendarEventTypeCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventTypeCommand, CreateCalendarEventTypeCommand>().ReverseMap();
        }
    }
}
