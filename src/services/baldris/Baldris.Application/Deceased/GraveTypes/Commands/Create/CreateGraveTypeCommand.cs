using System;
using AutoMapper;
using Baldris.Application.Deceased.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Commands.Create
{
    public class CreateGraveTypeCommand : IRequest<GraveTypeDto>, IHaveCustomMapping
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string InternalIdentifier { get; set; }
        public string IconName { get; set; }

        public Guid? ImageId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateGraveTypeCommand, GraveType>();

        }
    }
}
