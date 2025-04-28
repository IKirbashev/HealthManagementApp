using HealthApp.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public interface IHealthRecordService
    {
        Task<(List<HealthRecordDto> Items, int TotalCount)> GetAllAsync(DateTime? startDate, DateTime? endDate, string type, string keyword, int page, int pageSize);
        Task<HealthRecordDto> GetByIdAsync(Guid id);
        Task<HealthRecordDto> CreateAsync(HealthRecordDto dto);
        Task UpdateAsync(Guid id, HealthRecordDto dto);
        Task DeleteAsync(Guid id);
    }
}