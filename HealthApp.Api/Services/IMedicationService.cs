using HealthApp.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public interface IMedicationService
    {
        Task<List<MedicationDto>> GetAllAsync();
        Task<MedicationDto> GetByIdAsync(Guid id);
        Task<MedicationDto> CreateAsync(MedicationDto dto);
        Task UpdateAsync(Guid id, MedicationDto dto);
        Task DeleteAsync(Guid id);
        Task<MedicationIntakeDto> RecordIntakeAsync(Guid medicationId, MedicationIntakeDto dto);
    }
}