using System;
using AutoMapper;
using Baldris.Application.Deceased.Models;
using Baldris.Application.Deceased.GraveTypes.Commands.Create;
using Baldris.Application.Deceased.GraveTypes.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Commands.UpdateOrInsert
{
    public class UpdateOrInsertGraveTypeCommand : IRequest<GraveTypeDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string InternalIdentifier { get; set; }
        public string IconName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertGraveTypeCommand, GraveType>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertGraveTypeCommand, UpdateGraveTypeCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertGraveTypeCommand, CreateGraveTypeCommand>().ReverseMap();
        }
    }
}
