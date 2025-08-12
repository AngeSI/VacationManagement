using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using VacationDomain.Interfaces;
using VacationDomain.Services;

using VacationManagementAPI.Adapters;
using VacationManagementAPI.Dtos;

namespace VacationManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacationController : ControllerBase
    {

        private readonly ILogger<VacationController> _logger;
        IVacationService _vacationService;

        public VacationController(ILogger<VacationController> logger, IVacationService vacationService)
        {
            _logger = logger;
            _vacationService = vacationService;
        }


        /// <summary>
        /// Cr�ation d'une demande de cong�.
        /// </summary>
        /// <param name="employeeLogin">Login de l'employ�</param>
        /// <param name="startTime">Date de d�but du cong�s jj/mm/aaaa</param>
        /// <param name="endTime">Date de fin du cong�s jj/mm/aaaa</param>
        /// <param name="vacationType">Types de cong�s exemple : CongesPayes, RTT, Maladie, SansSolde, Autre</param>
        /// <param name="employeeComment">Commentaire de l'employ�</param>
        /// 
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        [HttpPost()]
        [Route("SubmitVacation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<int> SubmitVacation([Required] string employeeLogin, 
            [Required] string startTime, 
            [Required] string endTime, 
            [Required] string vacationType, 
            string? employeeComment)
        {
            
            // For now, we will just log the received data.
            _logger.LogInformation("Vacation submitted: {EmployeeLogin}, {StartTime}, {EndTime}, {VacationType}, {EmployeeComment}",
                employeeLogin, startTime, endTime, vacationType, employeeComment);

            // Here you would typically call a service to handle the vacation submission logic.
            // For example:
            IVacationSubmit vacationDto = MapToVacation.GetVacationDtoForCreation(employeeLogin:employeeLogin, startTime:startTime, endTime:endTime, vacationType:vacationType, employeeComment:employeeComment);
            return await _vacationService.CreateVacation(vacationDto);

        }

        /// <summary>
        /// Validation de la demande de cong�s.
        /// </summary>
        /// <param name="idVacation">Id de la demande de cong�s</param>
        /// <param name="hRLogin">login de la RH Gestionnaire</param>
        /// <param name="approved">True approve false reject</param>
        /// <param name="hRComment">Commentaires de la RH Gestionnaire</param>
        /// <returns></returns>
        [HttpPost()]
        [Route("ApproveVacation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<int> ApproveVacationSubmit([Required] int idVacation, 
            [Required] string hRLogin,
             [Required] bool approved,
             string? hRComment)
        {
            return await _vacationService.ValidateVacation(idVacation, hRLogin, approved, hRComment);
        }
    }
}
