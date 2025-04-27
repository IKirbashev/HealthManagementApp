using HealthApp.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public interface IDocumentService
    {
        Task<List<DocumentDto>> GetAllAsync(DateTime? startDate, DateTime? endDate, string category);
        Task<DocumentDto> GetByIdAsync(Guid id);
        Task<DocumentDto> CreateAsync(DocumentDto dto);
        Task UpdateAsync(Guid id, DocumentDto dto);
        Task DeleteAsync(Guid id);
    }
}