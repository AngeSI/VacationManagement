using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using VacationDomain.Interfaces;

namespace VacationManagementAPI.Dtos
{    
    /// <summary>
    /// VacationSubmitDto est utilisé pour soumettre une demande de congé.
    /// </summary>
    public record VacationSubmitDto : IVacationSubmit
    {
        /// <summary>
        /// login de l'employé
        /// </summary>
        public string? EmployeeLogin { get; set; }

        /// <summary>
        /// Commentaire de l'employé
        /// </summary>
        public string? EmployeeComment { get; set; }

        /// <summary>
        /// Type de vacances
        /// </summary>
        public string? VacationType { get; set; }

        /// <summary>
        /// Id de la demande de congé.
        /// </summary>
        public int VacationId { get; set; }

        /// <summary>
        /// Commentaire de la RH.
        /// </summary>
        public string? HRComment { get; set; }

        /// <summary>
        /// Login de la RH qui a validé la demande
        /// </summary>
        public string? ValidationHRLogin { get; set; }

        /// <summary>
        /// Statut d'approbation de la demande de congé.
        /// </summary>
        public bool? IsApproved { get; set; }

        /// <summary>
        /// Date de début du congé demandé par l'employé.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Date de fin du congé demandé par l'employé.
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
