using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VacationDomain.Interfaces;

namespace VacationDomainTests.TestModels
{
    internal class TestVacationSubmit : IVacationSubmit
    {
        public int VacationId { get; set; }
        public string? EmployeeLogin { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? EmployeeComment { get; set; }
        public string? HRComment { get; set; }
        public string? VacationType { get; set; }
        public bool? IsApproved { get; set; }
        public string? ValidationHRLogin { get; set; }
    }
}
