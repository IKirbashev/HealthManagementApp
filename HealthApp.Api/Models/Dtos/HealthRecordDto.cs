using System;
using System.Collections.Generic;

namespace HealthApp.Api.Models.Dtos
{
    public class HealthRecordDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Symptoms { get; set; }
        public string Recommendations { get; set; }
        public string CustomFields { get; set; }
        public List<HealthRecordFileDto> Files { get; set; }
    }
}