using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Commands.Create
{
    public class CreateCalendarEventDeceasedCommand : IRequest<CalendarEventDeceasedDto>, IHaveCustomMapping
    {
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public OpenCasket OpenCasket { get; set; }

        public Guid CalendarEventId { get; set; }

        public Guid DeceasedId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateCalendarEventDeceasedCommand, Entities.Common.CalendarEventDeceased>();

        }
    }
}
