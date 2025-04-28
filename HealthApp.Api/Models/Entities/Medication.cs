using System;
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
        public string DosageUnit { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(5)]
        public string IntakeTime { get; set; } // Например, "08:00"

        public int RepeatEveryNDays { get; set; } = 1;

        public string Notes { get; set; }

        public Guid UserId { get; set; }
    }
}