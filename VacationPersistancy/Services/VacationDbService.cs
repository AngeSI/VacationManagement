using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VacationDomain;
using VacationDomain.Interfaces;
using VacationDomain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using VacationDomain.Models;

namespace VacationPersistancy.Vacation
{
    public class VacationDbService : DbContext, IVacationDbService
    {
        public VacationDbService() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0");
        }
        public Task<int> CreateVacationSubmit(IVacationSubmit vacation)
        {
            throw new NotImplementedException();
        }

        public IEmployee GetEmployee(string employeeLogin)
        {
            throw new NotImplementedException();
        }

        public List<string> GetListOfVacationTypes()
        {
            throw new NotImplementedException();
        }

        public List<IVacationSubmit> GetVacationApprovedOrNotYet(string employeeLogin)
        {
            throw new NotImplementedException();
        }

        public IVacationSubmit GetVacationSubmit(int vacationId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveVacationSubmit(IVacationSubmit vacation)
        {
            throw new NotImplementedException();
        }
    }
}
