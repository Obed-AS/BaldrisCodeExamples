using System;
using AutoMapper;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Deceased.Deceased.Commands.Create;
using Baldris.Application.Deceased.Deceased.Commands.Update;
using Baldris.Application.Parties.Addresses.Commands.UpdateOrInsert;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Deceased.Deceased.Commands.UpdateOrInsert
{
    public class UpdateOrInsertDeceasedCommand : IRequest<DeceasedDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DateOfBirthApproximate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string DateOfDeathApproximate { get; set; }
        public string SocialSecurityNumber { get; set; }
        public Guid? ImageId { get; set; }

        public UpdateOrInsertAddressCommand UpdateOrInsertAddressCommand { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
        public string YearOfMarriage { get; set; }
        
        public Guid? MunicipalityOfResidenceId { get; set; }
        // public string MunicipalityOfResidenceName { get; set; }

        public Guid? MunicipalityOfBirthId { get; set; }
        // public string MunicipalityOfBirthName { get; set; }

        public Guid? MunicipalityOfDeathId { get; set; }
        // public string MunicipalityOfDeathName { get; set; }
        
        public Guid? PlaceOfDeathId { get; set; }
        // public string PlaceOfDeathName { get; set; }
        public string PlaceOfDeathRemarks { get; set; }

        public Guid? PlaceOfDeathAddressId { get; set; }
        // public string PlaceOfDeathAddressLocalizedAddress { get; set; }
        
        public Guid? InfectionStatusId { get; set; }
        // public string InfectionStatusLookupValue { get; set; }
        public string InfectionStatusRemarks { get; set; }
        
        public Guid? CasketId { get; set; }
        public CasketAttributes AppliedCasketAttributes { get; set; }
        // public string CasketName { get; set; }
        public string CasketRemarks { get; set; }
        
        public Guid? UrnId { get; set; }
        // public string UrnName { get; set; }
        public string UrnRemarks { get; set; }
        
        public Probate Probate { get; set; }
        public DeceasedLeavesBehind DeceasedLeavesBehind { get; set; }

        public string Assets { get; set; }
        
        public string Citizenship { get; set; }
        public Guid? CongregationId { get; set; }
        // public string CongregationDisplayName { get; set; }
        public Guid? ReligionId { get; set; }
        // public string ReligionDisplayName { get; set; }

        public string WorkTitle { get; set; }
        public string ClosestFamily { get; set; }

        public Implants Implants { get; set; }
        
        public DeceasedOptions Options { get; set; }
        
        public Guid? GraveId { get; set; }
        
        public string Remarks { get; set; }

        public Guid? TargetWorkOrderId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertDeceasedCommand, Entities.Common.Deceased>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertDeceasedCommand, UpdateDeceasedCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertDeceasedCommand, CreateDeceasedCommand>().ReverseMap();
        }
    }
}
