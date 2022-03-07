using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventEquipment.Commands.Update
{
    public class UpdateCalendarEventEquipmentCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }

        public Guid EquipmentId { get; set; }

        public Guid CalendarEventId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateCalendarEventEquipmentCommand, Entities.Common.CalendarEventEquipment>();
        }
    }
}
