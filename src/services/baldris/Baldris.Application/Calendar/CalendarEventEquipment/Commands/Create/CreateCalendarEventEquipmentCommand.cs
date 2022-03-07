using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Commands.Create
{
    public class CreateCalendarEventEquipmentCommand : IRequest<CalendarEventEquipmentDto>, IHaveCustomMapping
    {
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid EquipmentId { get; set; }

        public Guid CalendarEventId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateCalendarEventEquipmentCommand, Entities.Common.CalendarEventEquipment>();

        }
    }
}
