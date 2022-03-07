using System;
using System.Collections.Generic;
using Baldris.Application.Calendar.Models;
using MediatR;

namespace Baldris.Application.Calendar.Bookings.Queries.GetByConfigName
{
    public class GetBookingsByConfigNameQuery : IRequest<IEnumerable<BookingDto>>
    {
        public GetBookingsByConfigNameQuery(string configName, bool includeSoftDeleted = false, Guid? departmentId = null, Guid? legalUnitId = null, Guid? workOrderTypeId = null)
        {
            ConfigName = configName;
            IncludeSoftDeleted = includeSoftDeleted;
            DepartmentId = departmentId;
            LegalUnitId = legalUnitId;
            WorkOrderTypeId = workOrderTypeId;
        }

        public string ConfigName { get; }
        public Guid? DepartmentId { get; }
        public Guid? LegalUnitId { get; }
        public Guid? WorkOrderTypeId { get; }
        public bool IncludeSoftDeleted { get; }
    }
}
