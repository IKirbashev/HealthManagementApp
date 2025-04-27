using System;
using System.Collections.Generic;

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
        public string IntakeTimes { get; set; }
        public int RepeatEveryNDays { get; set; }
        public string Notes { get; set; }
        public List<MedicationIntakeDto> Intakes { get; set; }
    }
}