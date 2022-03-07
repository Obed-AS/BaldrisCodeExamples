using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventTypeProgramSlots.Commands.Update
{
    public class UpdateCalendarEventTypeProgramSlotCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid CalendarEventTypeId { get; set; }

        public Guid ProgramSlotId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateCalendarEventTypeProgramSlotCommand, CalendarEventTypeProgramSlot>();
        }
    }
}
