using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.ParticipantRoles.Commands.Create;
using Baldris.Application.Calendar.ParticipantRoles.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.ParticipantRoles.Commands.UpdateOrInsert
{
    public class UpdateOrInsertParticipantRoleCommand : IRequest<ParticipantRoleDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string TextAlternative { get; set; }
        public string EnglishText { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public string IconName { get; set; }
        public ParticipantRoleInternalType InternalType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertParticipantRoleCommand, ParticipantRole>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertParticipantRoleCommand, UpdateParticipantRoleCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertParticipantRoleCommand, CreateParticipantRoleCommand>().ReverseMap();
        }
    }
}
