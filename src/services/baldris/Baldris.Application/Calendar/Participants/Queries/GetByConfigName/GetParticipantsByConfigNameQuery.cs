using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Participants.Queries.GetByConfigName
{
    public class GetParticipantsByConfigNameQuery : IRequest<IEnumerable<ParticipantDto>>
    {
        public GetParticipantsByConfigNameQuery(string configName, bool includeSoftDeleted = false, Guid? departmentId = null, Guid? legalUnitId = null, Guid? workOrderTypeId = null)
        {
            ConfigName = configName;
			IncludeSoftDeleted = includeSoftDeleted;
            DepartmentId = departmentId;
            LegalUnitId = legalUnitId;
            WorkOrderTypeId = workOrderTypeId;
        }

        public string ConfigName { get; }
		public bool IncludeSoftDeleted { get; }
        public Guid? DepartmentId { get; }
        public Guid? LegalUnitId { get; }
        public Guid? WorkOrderTypeId { get; }
    }
}
