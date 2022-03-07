using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class TransportDto : CalendarEventDto, IHaveCustomMapping
    {
        public string ObjectType => typeof(TransportDto).ToString();
        public decimal? Distance { get; set; }
        public decimal? DriverHours { get; set; }
        public decimal? AdditionalHours { get; set; }
        public decimal? Price { get; set; }
        public decimal? AdditionalCost { get; set; }
        public string PriceRemarks { get; set; }

        public bool IsTransportTaxed { get; set; }

        public string TransportDescription { get; set; }

        public Guid? OriginId { get; set; }
        public string OriginName { get; set; }
        public string OriginRemarks { get; set; }
        public string OriginSerialized { get; set; }

        public Guid? DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string DestinationRemarks { get; set; }
        public string DestinationSerialized { get; set; }

        public int? HelpersFamily { get; set; }
        public int? HelpersFuneralHome { get; set; }
        public string HelpersRemarks { get; set; }

        public Guid? TransportById { get; set; }
        public string TransportByLookupValue { get; set; }
        public string TransportBySerialized { get; set; }

        public Guid? CasketEquipmentId { get; set; }
        public string CasketEquipmentLookupValue { get; set; }
        public string CasketEquipmentSerialized { get; set; }

        public string NoteOnPhysician { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Transport, TransportDto>()
                .IncludeBase<CalendarEvent, CalendarEventDto>();
        }
    }
}
