using System;
using System.Linq;
using AutoMapper;
using Baldris.Application.Parties.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Deceased.Models
{
    public class DeceasedDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DateOfBirthApproximate { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string DateOfDeathApproximate { get; set; }
        public string SocialSecurityNumber { get; set; }
        public Guid? ImageId { get; set; }

        public AddressDto Address { get; set; }

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
        
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Common.Deceased, DeceasedDto>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(target => target.PersonId, opt => opt.MapFrom(source => source.PersonId))
                .ForMember(target => target.FirstName, opt => opt.MapFrom(source => source.Person.FirstName))
                .ForMember(target => target.LastName, opt => opt.MapFrom(source => source.Person.LastName))
                .ForMember(target => target.Sex, opt => opt.MapFrom(source => source.Person.Sex))
                .ForMember(target => target.SocialSecurityNumber,
                    opt => opt.MapFrom(source => source.Person.SocialSecurityNumber))
                .ForMember(target => target.DateOfBirth, opt => opt.MapFrom(source => source.Person.DateOfBirth))
                .ForMember(target => target.DateOfBirthApproximate,
                    opt => opt.MapFrom(source => source.Person.DateOfBirthApproximate))
                .ForMember(target => target.DateOfDeath, opt => opt.MapFrom(source => source.Person.DateOfDeath))
                .ForMember(target => target.DateOfDeathApproximate,
                    opt => opt.MapFrom(source => source.Person.DateOfDeathApproximate))
                .ForMember(target => target.ImageId,
                    opt => opt.MapFrom(source => source.Person.ImageId))
                // .ForMember(target => target.Address,
                //    opt => opt.MapFrom(source =>
                //        source.Person.Addresses.OrderByDescending(x => x.IsDefault).ThenBy(x => x.SortOrder)
                //            .FirstOrDefault()))
                .ForMember(target => target.Address,
                    opt => opt.Ignore())
                .ForMember(target => target.MaritalStatus, opt => opt.MapFrom(source => source.MaritalStatus))
                .ForMember(target => target.YearOfMarriage, opt => opt.MapFrom(source => source.YearOfMarriage))
                .ForMember(target => target.MunicipalityOfBirthId,
                    opt => opt.MapFrom(source => source.MunicipalityOfBirthId))
                .ForMember(target => target.MunicipalityOfDeathId,
                    opt => opt.MapFrom(source => source.MunicipalityOfDeathId))
                .ForMember(target => target.MunicipalityOfResidenceId,
                    opt => opt.MapFrom(source => source.MunicipalityOfResidenceId))
                .ForMember(target => target.PlaceOfDeathId, opt => opt.MapFrom(source => source.PlaceOfDeathId))
                .ForMember(target => target.PlaceOfDeathRemarks,
                    opt => opt.MapFrom(source => source.PlaceOfDeathRemarks))
                .ForMember(target => target.PlaceOfDeathAddressId,
                    opt => opt.MapFrom(source => source.PlaceOfDeathAddressId))
                .ForMember(target => target.InfectionStatusId, opt => opt.MapFrom(source => source.InfectionStatusId))
                .ForMember(target => target.InfectionStatusRemarks,
                    opt => opt.MapFrom(source => source.InfectionStatusRemarks))
                .ForMember(target => target.CasketId, opt => opt.MapFrom(source => source.CasketId))
                .ForMember(target => target.AppliedCasketAttributes,
                    opt => opt.MapFrom(source => source.AppliedCasketAttributes))
                .ForMember(target => target.CasketRemarks, opt => opt.MapFrom(source => source.CasketRemarks))
                .ForMember(target => target.UrnId, opt => opt.MapFrom(source => source.UrnId))
                .ForMember(target => target.UrnRemarks, opt => opt.MapFrom(source => source.UrnRemarks))
                .ForMember(target => target.Probate, opt => opt.MapFrom(source => source.Probate))
                .ForMember(target => target.DeceasedLeavesBehind,
                    opt => opt.MapFrom(source => source.DeceasedLeavesBehind))
                .ForMember(target => target.Assets, opt => opt.MapFrom(source => source.Assets))
                .ForMember(target => target.Citizenship, opt => opt.MapFrom(source => source.Citizenship))
                .ForMember(target => target.CongregationId, opt => opt.MapFrom(source => source.CongregationId))
                .ForMember(target => target.ReligionId, opt => opt.MapFrom(source => source.ReligionId))
                .ForMember(target => target.WorkTitle, opt => opt.MapFrom(source => source.WorkTitle))
                .ForMember(target => target.ClosestFamily, opt => opt.MapFrom(source => source.ClosestFamily))
                .ForMember(target => target.Implants, opt => opt.MapFrom(source => source.Implants))
                .ForMember(target => target.Options, opt => opt.MapFrom(source => source.Options))
                .ForMember(target => target.GraveId, opt => opt.MapFrom(source => source.GraveId))
                .ForMember(target => target.Remarks, opt => opt.MapFrom(source => source.Remarks));
        }
    }
}