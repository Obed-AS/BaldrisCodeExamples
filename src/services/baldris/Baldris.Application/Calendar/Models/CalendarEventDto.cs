using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CalendarEventDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string ObjectType => typeof(CalendarEventDto).ToString();
        public string HostSerialized { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string DeceasedSerialized { get; set; }
        public string LocationSerialized { get; set; }
        public string AttendeesSerialized { get; set; }
        public DateTime? AttendanceTime { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsAllDay { get; set; }
        public string InternalType { get; set; }
        public Guid? InternalId { get; set; }
        public string RecurrencePattern { get; set; }
        public string ExceptionAppointments { get; set; }
        public bool AttendeesMustRsvp { get; set; }
        public string Remarks { get; set; }
        public CalendarEventOptions Options { get; set; }
        public BillTo BillTo { get; set; }

        public Guid? CalendarEventTypeId { get; set; }
        public string CalendarEventTypeName { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string CompletedById { get; set; }
        public string CompletedByDisplayName { get; set; }

        public Guid? WorkOrderId { get; set; }
        public string WorkOrderOrderCode { get; set; }

        public Guid? HostId { get; set; }
        public string HostDisplayName { get; set; }

        public Guid? ReservationId { get; set; }

        public Guid? LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationRemarks { get; set; }

        public Guid? AddressId { get; set; }
        public string AddressLocalizedAddress { get; set; }
        public string AddressRemarks { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CalendarEvent, CalendarEventDto>()
                .Include<Booking, BookingDto>()
                .Include<Ceremony, CeremonyDto>()
                .Include<Grooming, GroomingDto>()
                .Include<Transport, TransportDto>();
        }
    }
}