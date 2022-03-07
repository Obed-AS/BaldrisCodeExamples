using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Deceased.Models
{
    public class GrantApplicationDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public GrantApplicationOptions Options { get; set; }
        public decimal? FuneralCost { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? OtherCost { get; set; }
        public decimal? GovernmentCost { get; set; }
        public decimal? HeadstoneCost { get; set; }
        public decimal? Assets { get; set; }
        public decimal? InsurancePayout { get; set; }
        public decimal? PensionPayout { get; set; }
        public decimal? OtherPayout { get; set; }

        public string UnionForm { get; set; }
        public string WorkPlace { get; set; }
        public string Remarks { get; set; }

        public Guid? DeceasedId { get; set; }
        public string DeceasedName { get; set; }

        public Guid? WorkOrderId { get; set; }
        public string WorkOrderCode { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GrantApplication, GrantApplicationDto>()
                .ForMember(target => target.DeceasedName,
                    opt => opt.MapFrom(source => source.Deceased.Person.DisplayName))
                .ForMember(target => target.WorkOrderCode,
                    opt => opt.MapFrom(source => source.WorkOrder.OrderCode));
        }
    }
}
