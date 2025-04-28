using System;

namespace HealthApp.Api.Models.Dtos
{
    public class MedicationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal DosageValue { get; set; }
        public string DosageUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string IntakeTime { get; set; }
        public int RepeatEveryNDays { get; set; }
        public string Notes { get; set; }
    }
}