using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventDeceased.Commands.Update
{
    public class UpdateCalendarEventDeceasedCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public OpenCasket OpenCasket { get; set; }

        public Guid CalendarEventId { get; set; }

        public Guid DeceasedId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateCalendarEventDeceasedCommand, Entities.Common.CalendarEventDeceased>();
        }
    }
}
