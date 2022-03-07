using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.Bookings.Commands.Create;
using Baldris.Application.Calendar.Bookings.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Commands.UpdateOrInsert
{
    public class UpdateOrInsertBookingCommand : IRequest<BookingDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
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

        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string CompletedById { get; set; }

        public Guid? WorkOrderId { get; set; }

        public Guid? HostId { get; set; }

        public Guid? ReservationId { get; set; }

        public Guid? LocationId { get; set; }
        public string LocationRemarks { get; set; }

        public Guid? AddressId { get; set; }
        public string AddressRemarks { get; set; }
        public Guid? SupplierId { get; set; }
        public string RemarksSupplier { get; set; }

        public string ConfirmedById { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationRemarks { get; set; }

        public string ExternalId { get; set; }
        public string ExternalOrderId { get; set; }
        public int? ExternalVersion { get; set; }
        public string ExternalStatus { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertBookingCommand, Booking>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertBookingCommand, UpdateBookingCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertBookingCommand, CreateBookingCommand>().ReverseMap();
        }
    }
}
