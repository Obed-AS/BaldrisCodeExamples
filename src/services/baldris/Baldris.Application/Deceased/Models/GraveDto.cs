using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Deceased.Models
{
    public class GraveDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string GraveNumber { get; set; }
        public string GraveLeaseNumber { get; set; }
        public bool IsAnonymousGrave { get; set; }
        public bool GraveShouldBeOnNewField { get; set; }
        public string Remarks { get; set; }

        public Guid? LeaseHolderId { get; set; }
        public string LeaseHolderName { get; set; }
        public string LeaseHolderRemarks { get; set; }
        public bool LeaseHolderHasOtherLeases { get; set; }
        public string LeaseHolderOtherLeasesRemarks { get; set; }

        public Guid? TemporaryMarkerId { get; set; }
        public string TemporaryMarkerType { get; set; }
        public string TemporaryMarkerPlaqueName { get; set; }
        public string TemporaryMarkerRemarks { get; set; }

        public Guid? LeaseTransferId { get; set; }
        public string LeaseTransferRemarks { get; set; }
        public string LeaseTransferSerialized { get; set; }

        public Guid? GraveTypeId { get; set; }
        public string GraveTypeName { get; set; }

        public Guid? CemeteryId { get; set; }
        public string CemeteryName { get; set; }

        public Guid? PreviouslyBuriedId { get; set; }
        public string PreviouslyBuriedName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Grave, GraveDto>()
                .ForMember(target => target.LeaseHolderName,
                    opt => opt.MapFrom(source => source.LeaseHolder.DisplayName))
                .ForMember(target => target.TemporaryMarkerType,
                    opt => opt.MapFrom(source => source.TemporaryMarker.LookupValue))
                .ForMember(target => target.TemporaryMarkerPlaqueName,
                    opt => opt.MapFrom(source => source.TemporaryMarkerName))
                .ForMember(target => target.LeaseTransferRemarks,
                    opt => opt.MapFrom(source => source.LeaseTransfer.LookupValue))
                .ForMember(target => target.GraveTypeName,
                    opt => opt.MapFrom(source => source.GraveType.Name))
                .ForMember(target => target.CemeteryName,
                    opt => opt.MapFrom(source => source.Cemetery.Name))
                .ForMember(target => target.PreviouslyBuriedName,
                    opt => opt.MapFrom(source => source.PreviouslyBuried.DisplayName));
        }
    }
}
