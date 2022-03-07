using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Deceased.Graves.Commands.Update
{
    public class UpdateGraveCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string GraveNumber { get; set; }
        public string GraveLeaseNumber { get; set; }
        public bool IsAnonymousGrave { get; set; }
        public bool GraveShouldBeOnNewField { get; set; }
        public string Remarks { get; set; }

        public Guid? LeaseHolderId { get; set; }
        public string LeaseHolderRemarks { get; set; }
        public bool LeaseHolderHasOtherLeases { get; set; }
        public string LeaseHolderOtherLeasesRemarks { get; set; }

        public Guid? TemporaryMarkerId { get; set; }
        public string TemporaryMarkerPlaqueName { get; set; }
        public string TemporaryMarkerRemarks { get; set; }

        public Guid? LeaseTransferId { get; set; }
        public string LeaseTransferSerialized { get; set; }

        public Guid? GraveTypeId { get; set; }

        public Guid? CemeteryId { get; set; }

        public Guid? PreviouslyBuriedId { get; set; }
        
        public Guid? TargetWorkOrderId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateGraveCommand, Grave>()
                .ForMember(target => target.TemporaryMarkerName,
                    opt => opt.MapFrom(source => source.TemporaryMarkerPlaqueName));
        }
    }
}
