using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Groomings.Commands.Update
{
    public class UpdateGroomingCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
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
        public Guid? ClothingId { get; set; }
        public string ClothingSerialized { get; set; }

        public string PersonalClothing { get; set; }
        public string PersonalBelongings { get; set; }
        public string GroomingDescription { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateGroomingCommand, Grooming>();
        }
    }
}
