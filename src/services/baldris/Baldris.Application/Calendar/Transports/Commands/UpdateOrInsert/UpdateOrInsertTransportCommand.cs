using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.Transports.Commands.Create;
using Baldris.Application.Calendar.Transports.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Transports.Commands.UpdateOrInsert
{
    public class UpdateOrInsertTransportCommand : IRequest<TransportDto>, IHaveCustomMapping
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
        public decimal? Distance { get; set; }
        public decimal? DriverHours { get; set; }
        public decimal? AdditionalHours { get; set; }
        public decimal? Price { get; set; }
        public decimal? AdditionalCost { get; set; }
        public string PriceRemarks { get; set; }

        public bool IsTransportTaxed { get; set; }

        public string TransportDescription { get; set; }

        public Guid? OriginId { get; set; }
        public string OriginRemarks { get; set; }
        public string OriginSerialized { get; set; }

        public Guid? DestinationId { get; set; }
        public string DestinationRemarks { get; set; }
        public string DestinationSerialized { get; set; }

        public int? HelpersFamily { get; set; }
        public int? HelpersFuneralHome { get; set; }
        public string HelpersRemarks { get; set; }

        public Guid? TransportById { get; set; }
        public string TransportBySerialized { get; set; }

        public Guid? CasketEquipmentId { get; set; }
        public string CasketEquipmentSerialized { get; set; }

        public string NoteOnPhysician { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertTransportCommand, Transport>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertTransportCommand, UpdateTransportCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertTransportCommand, CreateTransportCommand>().ReverseMap();
        }
    }
}
