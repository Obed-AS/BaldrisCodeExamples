using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.Create
{
    public class CreateCalendarEventTypeProgramSlotCommand : IRequest<CalendarEventTypeProgramSlotDto>, IHaveCustomMapping
    {
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public Guid ProgramSlotId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateCalendarEventTypeProgramSlotCommand, CalendarEventTypeProgramSlot>();

        }
    }
}
