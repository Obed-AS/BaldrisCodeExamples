using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class ParticipantRoleDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
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
            configuration.CreateMap<ParticipantRole, ParticipantRoleDto>();
        }
    }
}
