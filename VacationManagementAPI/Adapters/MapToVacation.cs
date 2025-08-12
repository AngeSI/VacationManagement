using VacationManagementAPI.Dtos;
using VacationDomain.Interfaces;
using VacationDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VacationManagementAPI.Adapters
{
    /// <summary>
    /// Mapper class to convert vacation DTOs to domain models.
    /// </summary>
    public static class MapToVacation
    {
        /// <summary>
        /// Convertis a vacation DTO to an IVacationSubmit model for creation
        /// </summary>
        /// <param name="employeeLogin"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="vacationType"></param>
        /// <param name="employeeComment"></param>
        /// <returns></returns>
        public static IVacationSubmit GetVacationDtoForCreation(string employeeLogin,
            string startTime,
            string endTime,
            string vacationType,
            string? employeeComment)
        {
            return new VacationSubmitDto
            {
                EmployeeLogin = employeeLogin,
                EmployeeComment = employeeComment,
                EndTime = DateTime.TryParse(endTime, out var mendTime) ? mendTime : null,
                StartTime = DateTime.TryParse(startTime, out var mstartTime) ? mstartTime : null,
                VacationType = vacationType
            };
        }
        /// <summary>
        /// Mapper to convert a vacation DTO to an IVacationSubmit model for approval
        /// </summary>
        /// <param name="idVacation"></param>
        /// <param name="hRLogin"></param>
        /// <param name="approved"></param>
        /// <param name="hRComment"></param>
        /// <returns></returns>
        public static IVacationSubmit GetVacationDtoForApproval(int idVacation,
            string hRLogin,
            bool approved,
            string? hRComment)
        {
            return new VacationSubmitDto { HRComment = hRComment, IsApproved = approved, VacationId = idVacation, ValidationHRLogin = hRLogin };
        }
    }
}
