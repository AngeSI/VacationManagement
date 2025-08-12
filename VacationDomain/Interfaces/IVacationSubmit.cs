using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationDomain.Interfaces
{
    public interface IVacationSubmit
    {
        /// <summary>
        /// Id de la demande de congé.
        /// </summary>
        public int VacationId { get; set; }

        /// <summary>
        /// Login de l'employé qui a soumis la demande de congé.
        /// </summary>
        public string? EmployeeLogin { get; set; }

        /// <summary>
        /// Date de début du congé demandé par l'employé.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Date de fin du congé demandé par l'employé.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Type de congé demandé par l'employé.
        /// </summary>
        public string? VacationType { get; set; }

        /// <summary>
        /// Commentaire de l'employé sur la demande de congé.
        /// </summary>
        public string? EmployeeComment { get; set; }

        /// <summary>
        /// Commentaire de la RH sur la demande de congé.
        /// </summary>
        public string? HRComment { get; set; }

        /// <summary>
        /// Login de la RH qui a validé la demande de congé.
        /// </summary>
        public string? ValidationHRLogin { get; set; }

        /// <summary>
        /// Statut d'approbation de la demande de congé.
        /// </summary>
        public bool? IsApproved { get; set; }

    }
}
