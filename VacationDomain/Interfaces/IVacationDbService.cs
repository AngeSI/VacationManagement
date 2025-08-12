using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationDomain.Models;
using VacationDomain.Services;

namespace VacationDomain.Interfaces
{
    public interface IVacationDbService
    {
        public Task<int> CreateVacationSubmit(IVacationSubmit vacation);

        public Task<int> SaveVacationSubmit(IVacationSubmit vacation);


        public List<IVacationSubmit> GetVacationApprovedOrNotYet(string employeeLogin);

        public IVacationSubmit GetVacationSubmit(int vacationId);

        IEmployee GetEmployee(string employeeLogin);

        /// <summary>
        /// List of vacation types available in the system. We can imagine that it is coded to simplify the process of adding new vacation types in the future.
        /// </summary>
        /// <returns></returns>
        List<string> GetListOfVacationTypes();
    }
}
