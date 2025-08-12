using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationDomain.Models;

namespace VacationDomainTests.TestModels
{
    internal class TestEmployee : IEmployee
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public bool IsHR { get; set; }
        public Dictionary<string, int> VacationAccounts { get; set; } = new Dictionary<string, int>();
    }
}
