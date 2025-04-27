using System;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class HealthRecordFile
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid HealthRecordId { get; set; }

        public HealthRecord HealthRecord { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(10)]
        public string FileType { get; set; } // Например, "PDF", "JPEG"
    }
}