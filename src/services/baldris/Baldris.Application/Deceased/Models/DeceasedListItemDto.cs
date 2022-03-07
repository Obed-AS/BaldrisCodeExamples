using System;
using AutoMapper;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Deceased.Models {
    public class DeceasedListItemDto : IHaveCustomMapping {
        public Guid DeceasedId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string PlaceOfDeath { get; set; }
        public string PlaceOfBirth { get; set; }

        public void CreateMappings(Profile configuration) {
            configuration.CreateMap<Baldris.Entities.Common.Deceased, DeceasedListItemDto>()
                .ForMember(DLIModel => DLIModel.FirstName, opt => opt.MapFrom(d => d.Person.FirstName))
                .ForMember(DLIModel => DLIModel.LastName, opt => opt.MapFrom(d => d.Person.LastName))
                .ForMember(DLIModel => DLIModel.DisplayName, opt => opt.MapFrom(d => d.Person.DisplayName))
                .ForMember(DLIModel => DLIModel.DateOfBirth, opt => opt.MapFrom(d => d.Person.DateOfBirth))
                .ForMember(DLIModel => DLIModel.DateOfDeath, opt => opt.MapFrom(d => d.Person.DateOfDeath))
                .ForMember(DLIModel => DLIModel.SocialSecurityNumber, opt => opt.MapFrom(d => d.Person.SocialSecurityNumber))
                .ForMember(DLIModel => DLIModel.PlaceOfDeath, opt => opt.MapFrom(d => d.PlaceOfDeathId.HasValue ? (d.PlaceOfDeath.Name + (d.MunicipalityOfBirthId.HasValue ? " (" + d.MunicipalityOfBirth.Name + ")" : "")) : (d.MunicipalityOfBirthId.HasValue ? d.MunicipalityOfBirth.Name : null)))
                .ForMember(DLIModel => DLIModel.DeceasedId, opt => opt.MapFrom(d => d.Id));
        }
    }
}