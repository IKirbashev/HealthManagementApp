using System;

namespace HealthApp.Api.Models.Dtos
{
    public class HealthRecordFileDto
    {
        public Guid Id { get; set; }
        public Guid HealthRecordId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}