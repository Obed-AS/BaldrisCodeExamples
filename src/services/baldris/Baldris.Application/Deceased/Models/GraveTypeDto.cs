using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Deceased.Models
{
    public class GraveTypeDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRequiredBySystem { get; set; }
        public bool IsInternal { get; set; }
        public bool IsDeleted { get; set; }
        public string InternalIdentifier { get; set; }
        public string IconName { get; set; }

        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GraveType, GraveTypeDto>()
                .ForMember(target => target.ImageUrl,
                    opt => opt.MapFrom(source => source.Image.Filepath));
        }
    }
}
