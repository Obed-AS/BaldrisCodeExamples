using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventDeceased.Commands.Create;
using Baldris.Application.Calendar.CalendarEventDeceased.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventDeceasedCommand : IRequest<CalendarEventDeceasedDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public OpenCasket OpenCasket { get; set; }

        public Guid CalendarEventId { get; set; }

        public Guid DeceasedId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventDeceasedCommand, Entities.Common.CalendarEventDeceased>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventDeceasedCommand, UpdateCalendarEventDeceasedCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventDeceasedCommand, CreateCalendarEventDeceasedCommand>().ReverseMap();
        }
    }
}
