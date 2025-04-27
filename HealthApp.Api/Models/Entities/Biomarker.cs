using System;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class Biomarker
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Например, "глюкоза"

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } // Например, "мг/дл"

        public decimal? NormalRangeMin { get; set; }

        public decimal? NormalRangeMax { get; set; }

        public string Comments { get; set; }

        public Guid UserId { get; set; }
    }
}