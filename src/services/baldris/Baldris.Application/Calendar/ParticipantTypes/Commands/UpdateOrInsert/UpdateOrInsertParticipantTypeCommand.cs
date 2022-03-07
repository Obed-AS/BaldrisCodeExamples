using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.ParticipantTypes.Commands.Create;
using Baldris.Application.Calendar.ParticipantTypes.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantTypes.Commands.UpdateOrInsert
{
    public class UpdateOrInsertParticipantTypeCommand : IRequest<ParticipantTypeDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
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

        public Guid? DefaultParticipantRoleId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertParticipantTypeCommand, ParticipantType>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertParticipantTypeCommand, UpdateParticipantTypeCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertParticipantTypeCommand, CreateParticipantTypeCommand>().ReverseMap();
        }
    }
}
