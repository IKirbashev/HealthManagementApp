using System;

namespace HealthApp.Api.Models.Dtos
{
    public class MedicationIntakeDto
    {
        public Guid Id { get; set; }
        public Guid MedicationId { get; set; }
        public DateTime IntakeTime { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}