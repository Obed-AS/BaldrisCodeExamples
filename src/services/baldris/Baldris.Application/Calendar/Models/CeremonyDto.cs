using System;
using AutoMapper;
using Baldris.Entities.Common;
using BitFrost.Core.Interfaces;

namespace Baldris.Application.Calendar.Models
{
    public class CeremonyDto : CalendarEventDto, IHaveCustomMapping
    {
        public string ObjectType => typeof(CeremonyDto).ToString();
        public bool IsSilentCeremony { get; set; }
        public bool IsCeremonialFuneral { get; set; }
        public string Liturgy { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Ceremony, CeremonyDto>()
                .IncludeBase<CalendarEvent, CalendarEventDto>();
        }
    }
}
