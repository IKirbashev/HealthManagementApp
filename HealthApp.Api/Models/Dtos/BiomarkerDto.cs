using System;

namespace HealthApp.Api.Models.Dtos
{
    public class BiomarkerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Unit { get; set; }
        public decimal? NormalRangeMin { get; set; }
        public decimal? NormalRangeMax { get; set; }
        public string Comments { get; set; }
    }
}