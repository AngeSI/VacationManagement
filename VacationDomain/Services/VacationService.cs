using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VacationDomain.Interfaces;
using VacationDomain.Models;

namespace VacationDomain.Services
{
    public class VacationService : IVacationService
    {
        IVacationDbService _vacationDbService;

        public VacationService(IVacationDbService vacationDbService)
        {
            _vacationDbService = vacationDbService;
        }
        public async Task<int> CreateVacation(IVacationSubmit vacation)
        {
            if (vacation == null)
            {
                throw new ArgumentNullException(nameof(vacation), "Vacation cannot be null");
            }
            if (string.IsNullOrWhiteSpace(vacation.EmployeeLogin))
            {
                throw new ArgumentException("Employee login cannot be empty", nameof(vacation.EmployeeLogin));
            }
            if (!vacation.StartTime.HasValue)
            {
                throw new ArgumentException("Start time cannot be empty", nameof(vacation.StartTime));
            }
            if (!vacation.EndTime.HasValue)
            {
                throw new ArgumentException("End time cannot be empty", nameof(vacation.EmployeeLogin));
            }
            if (vacation.StartTime > vacation.EndTime)
            {
                throw new ArgumentException("Start time must be before or equal to end time", nameof(vacation));
            }
            //test if employee exists
            IEmployee employee = _vacationDbService.GetEmployee(vacation.EmployeeLogin);
            if (employee == null || string.IsNullOrEmpty(employee.Login))
            {
                throw new ArgumentException("Employee does not exist", nameof(vacation.EmployeeLogin));
            }

            if (vacation.EmployeeComment?.Length > 100)
            {
                throw new ArgumentException("Comment must be smaller than 100", nameof(vacation.EmployeeComment));
            }
            //test get list of vacation for employee and check if there is already a vacation in the same period
            if (_vacationDbService.GetVacationApprovedOrNotYet(vacation.EmployeeLogin).Where(t => !t.IsApproved.HasValue || t.IsApproved.Value)
                .Any(v => v.StartTime < vacation.EndTime && v.EndTime > vacation.StartTime))
            {
                throw new InvalidOperationException("There is already a vacation in the same period");
            }
            //Check if vacation type is valid
            if (string.IsNullOrWhiteSpace(vacation.VacationType))
            {
                throw new ArgumentException("Vacation type cannot be empty", nameof(vacation.VacationType));
            }
            //check if vacation type exists 
            if(_vacationDbService.GetListOfVacationTypes().Any(x => x.Equals(vacation.VacationType)))
            {                 
                throw new ArgumentException("Vacation type does not exist", nameof(vacation.VacationType));
            }

            //Check if employee has enoough vacation days
            // length of vacation in days
            int vacationDays = (vacation.EndTime.Value - vacation.StartTime.Value).Days + 1; // +1 because both start and end are inclusive
            if (employee.VacationAccounts.TryGetValue(vacation.VacationType, out int account) && account < vacationDays)
            {
                throw new InvalidOperationException("There is not enough vacation days for this vacation type");
            }

            try
            {
                return await _vacationDbService.CreateVacationSubmit(vacation);
            }
            catch (Exception e)
            {
                throw new Exception($"Error while saving vacation {e.Message}", e);
            }

        }

        public async Task<int> ValidateVacation(int vacationId, string hRLogin, bool isApproved, string? hRComment)
        {
            //test if vacation exists
            IVacationSubmit vacation = _vacationDbService.GetVacationSubmit(vacationId);

            if (vacation == null)
            {
                throw new Exception("Vacation does not exist");
            }
            if (string.IsNullOrWhiteSpace(hRLogin))
            {
                throw new ArgumentException("HR login cannot be empty", nameof(hRLogin));
            }
            if (hRLogin.Equals(vacation.EmployeeLogin))
            {
                throw new ArgumentException("HR login cannot be the same as employee login", nameof(hRLogin));
            }
            IEmployee hR = _vacationDbService.GetEmployee(hRLogin);
            if (hR == null)
            {
                throw new ArgumentException("HR does not exist", nameof(hRLogin));
            }
            if (hR.IsHR == false)
            {
                throw new ArgumentException("HR is not a HR", nameof(hRLogin));
            }
            if (vacation.IsApproved.HasValue)
            {
                throw new InvalidOperationException("Vacation has already been validated or rejected");
            }
            if (hRComment?.Length > 100)
            {
                throw new ArgumentException("Comment must be smaller than 100", nameof(hRComment));
            }
            vacation.IsApproved = isApproved;
            vacation.HRComment = hRComment;

            try
            {
                return await _vacationDbService.SaveVacationSubmit(vacation);
            }
            catch (Exception e)
            {
                throw new Exception($"Error while approving or rejecting vacation {e.Message}", e);
            }

        }

    }
}
