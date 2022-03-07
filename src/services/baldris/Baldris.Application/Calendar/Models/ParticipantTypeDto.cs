using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class ParticipantTypeDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultInternalType { get; set; }
        public string DefaultTitle { get; set; }
        public string DefaultProp { get; set; }
        public string PropLabel { get; set; }
        public int SortOrder { get; set; }
        public bool IsRequiredBySystem { get; set; }
        public bool IsInternal { get; set; }
        public bool IsDeleted { get; set; }
        public string IconName { get; set; }

        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }

        public Guid? DefaultParticipantRoleId { get; set; }
        public string DefaultParticipantRoleName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ParticipantType, ParticipantTypeDto>()
                .ForMember(target => target.ImageUrl,
                    opt => opt.MapFrom(source => source.Image.Filepath));
        }
    }
}
