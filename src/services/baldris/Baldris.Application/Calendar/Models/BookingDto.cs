using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class BookingDto : CalendarEventDto, IHaveCustomMapping
    {
        public string ObjectType => typeof(BookingDto).ToString();
        public Guid? SupplierId { get; set; }
        public string SupplierPartyDisplayName { get; set; }
        public string RemarksSupplier { get; set; }

        public string ConfirmedById { get; set; }
        public string ConfirmedByDisplayName { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationRemarks { get; set; }

        public string ExternalId { get; set; }
        public string ExternalOrderId { get; set; }
        public int? ExternalVersion { get; set; }
        public string ExternalStatus { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Booking, BookingDto>()
                .IncludeBase<CalendarEvent, CalendarEventDto>();
        }
    }
}
