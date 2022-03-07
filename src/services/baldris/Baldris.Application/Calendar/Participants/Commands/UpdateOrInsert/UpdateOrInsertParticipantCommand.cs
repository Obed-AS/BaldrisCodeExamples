using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.Participants.Commands.Create;
using Baldris.Application.Calendar.Participants.Commands.Update;
using Baldris.Application.Parties.Parties.Commands.UpdateOrInsert;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Commands.UpdateOrInsert
{
    public class UpdateOrInsertParticipantCommand : IRequest<ParticipantDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
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
            configuration.CreateMap<UpdateOrInsertParticipantCommand, Participant>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertParticipantCommand, UpdateParticipantCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertParticipantCommand, CreateParticipantCommand>().ReverseMap();
        }
    }
}
