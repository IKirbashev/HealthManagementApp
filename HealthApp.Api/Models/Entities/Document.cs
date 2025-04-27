using System;
using System.ComponentModel.DataAnnotations;

namespace HealthApp.Api.Models.Entities
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(10)]
        public string FileType { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } // Например, "анализы", "рецепты"

        [Required]
        public DateTime UploadDate { get; set; }

        public Guid UserId { get; set; }
    }
}