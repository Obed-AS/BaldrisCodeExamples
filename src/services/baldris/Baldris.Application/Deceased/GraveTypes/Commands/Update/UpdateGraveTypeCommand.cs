using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Deceased.GraveTypes.Commands.Update
{
    public class UpdateGraveTypeCommand : IRequest<int>, IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string InternalIdentifier { get; set; }
        public string IconName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateGraveTypeCommand, GraveType>();
        }
    }
}
