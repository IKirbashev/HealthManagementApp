using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class Medication
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal DosageValue { get; set; }

        [Required]
        [StringLength(20)]
        public string DosageUnit { get; set; } // Например, "мг"

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string IntakeTimes { get; set; } // JSON, например, ["08:00", "20:00"]

        public int RepeatEveryNDays { get; set; } = 1; // Повторение через N дней (по умолчанию 1 — ежедневно)

        public string Notes { get; set; }

        public Guid UserId { get; set; }

        public List<MedicationIntake> Intakes { get; set; } = new List<MedicationIntake>();
    }
}