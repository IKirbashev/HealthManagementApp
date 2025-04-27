using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class HealthRecord
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } // Например, "визит", "симптомы"

        public string Symptoms { get; set; }

        public string Recommendations { get; set; }

        public string CustomFields { get; set; } // JSON для пользовательских полей

        public Guid UserId { get; set; } // Для будущей авторизации

        public List<HealthRecordFile> Files { get; set; } = new List<HealthRecordFile>();
    }
}