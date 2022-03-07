using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Parties.Parties.Commands.UpdateOrInsert;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Commands.Create
{
    public class CreateParticipantCommand : IRequest<ParticipantDto>, IHaveCustomMapping
    {
        public string Title { get; set; }
        public string Prop { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public string Info { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool AttachedProductsOverridesTypeProducts { get; set; }

        public Guid PartyId { get; set; }
        public UpdateOrInsertPartyCommand UpdateOrInsertPartyCommand { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateParticipantCommand, Participant>();

        }
    }
}
