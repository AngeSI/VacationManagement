using System.ComponentModel.DataAnnotations;

using VacationDomain.Interfaces;

namespace VacationDomain.Services
{
    public interface IVacationService
    {
        Task<int> CreateVacation(IVacationSubmit vacation);

        Task<int> ValidateVacation(int VacationId, string hRLogin, bool isApproved, string? HRComment);
    }
}