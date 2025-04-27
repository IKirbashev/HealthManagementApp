using System;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class MedicationIntake
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid MedicationId { get; set; }

        public Medication Medication { get; set; }

        [Required]
        public DateTime IntakeTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Например, "принято", "пропущено"

        public string Reason { get; set; } // Причина пропуска
    }
}