using HealthApp.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public interface IBiomarkerService
    {
        Task<List<BiomarkerDto>> GetAllAsync(DateTime? startDate, DateTime? endDate, string name);
        Task<BiomarkerDto> GetByIdAsync(Guid id);
        Task<BiomarkerDto> CreateAsync(BiomarkerDto dto);
        Task UpdateAsync(Guid id, BiomarkerDto dto);
        Task DeleteAsync(Guid id);
        Task<List<BiomarkerDto>> GetTrendsAsync(string name, DateTime startDate, DateTime endDate);
    }
}