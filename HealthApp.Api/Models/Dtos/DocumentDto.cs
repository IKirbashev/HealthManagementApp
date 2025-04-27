using System;

namespace HealthApp.Api.Models.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Category { get; set; }
        public DateTime UploadDate { get; set; }
    }
}