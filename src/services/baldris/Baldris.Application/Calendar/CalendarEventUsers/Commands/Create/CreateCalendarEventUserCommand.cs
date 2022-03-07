using System;
using AutoMapper;
using Baldris.Application.Calendar.Models;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEventUsers.Commands.Create
{
    public class CreateCalendarEventUserCommand : IRequest<CalendarEventUserDto>, IHaveCustomMapping
    {
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
            configuration.CreateMap<CreateCalendarEventUserCommand, CalendarEventUser>();

        }
    }
}
