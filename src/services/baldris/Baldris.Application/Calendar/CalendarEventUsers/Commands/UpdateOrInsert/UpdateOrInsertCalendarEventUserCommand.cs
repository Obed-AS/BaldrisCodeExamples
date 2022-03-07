using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Application.Calendar.CalendarEventUsers.Commands.Create;
using Baldris.Application.Calendar.CalendarEventUsers.Commands.Update;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Commands.UpdateOrInsert
{
    public class UpdateOrInsertCalendarEventUserCommand : IRequest<CalendarEventUserDto>, IHaveCustomMapping
    {
        public Guid? Id { get; set; }
        public int SortOrder { get; set; }
        public string Role { get; set; }
        public string Remarks { get; set; }
        public RsvpStatus RsvpStatus { get; set; }
        public DateTime? RsvpChangedTime { get; set; }
        public string RsvpMessage { get; set; }

        public Guid CalendarEventId { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UpdateOrInsertCalendarEventUserCommand, CalendarEventUser>()
                .ForMember(target => target.Id, opt => opt.MapFrom(source => source.Id ?? Guid.Empty));
            configuration.CreateMap<UpdateOrInsertCalendarEventUserCommand, UpdateCalendarEventUserCommand>().ReverseMap();
            configuration.CreateMap<UpdateOrInsertCalendarEventUserCommand, CreateCalendarEventUserCommand>().ReverseMap();
        }
    }
}
