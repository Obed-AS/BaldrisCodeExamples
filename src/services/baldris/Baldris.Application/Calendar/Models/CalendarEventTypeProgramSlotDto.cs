using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventTypeProgramSlotDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid CalendarEventTypeId { get; set; }
        public string CalendarEventTypeName { get; set; }

        public Guid ProgramSlotId { get; set; }
        public string ProgramSlotName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CalendarEventTypeProgramSlot, CalendarEventTypeProgramSlotDto>();
        }
    }
}
