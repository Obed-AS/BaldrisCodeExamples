using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.CalendarEvents.Queries.GetByConfigName
{
    public class GetCalendarEventsByConfigNameQuery : IRequest<IEnumerable<CalendarEventDto>>
    {
        public GetCalendarEventsByConfigNameQuery(string configName, Guid? departmentId = null, Guid? legalUnitId = null, Guid? workOrderTypeId = null)
        {
            ConfigName = configName;
            DepartmentId = departmentId;
            LegalUnitId = legalUnitId;
            WorkOrderTypeId = workOrderTypeId;
        }

        public string ConfigName { get; }
        public Guid? DepartmentId { get; }
        public Guid? LegalUnitId { get; }
        public Guid? WorkOrderTypeId { get; }
    }
}
