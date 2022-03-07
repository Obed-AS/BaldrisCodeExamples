using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Ceremonies.Commands.Create
{
    public class CreateCeremonyCommand : IRequest<CeremonyDto>, IHaveCustomMapping
    {
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
        public bool IsSilentCeremony { get; set; }
        public bool IsCeremonialFuneral { get; set; }
        public string Liturgy { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateCeremonyCommand, Ceremony>();

        }
    }
}
