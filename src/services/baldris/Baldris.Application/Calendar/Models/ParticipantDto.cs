using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class ParticipantDto : IHaveCustomMapping
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Prop { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public string Info { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool AttachedProductsOverridesTypeProducts { get; set; }

        public Guid PartyId { get; set; }
        public string PartyDisplayName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Participant, ParticipantDto>();
        }
    }
}
