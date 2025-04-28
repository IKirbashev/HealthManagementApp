using HealthApp.Api.Data.Repositories;
using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IRepository<HealthRecord> _repository;

        public HealthRecordService(IRepository<HealthRecord> repository)
        {
            _repository = repository;
        }

        public async Task<(List<HealthRecordDto> Items, int TotalCount)> GetAllAsync(DateTime? startDate, DateTime? endDate, string type, string keyword, int page, int pageSize)
        {
            var query = await _repository.GetAllAsync();

            if (startDate.HasValue)
                query = query.Where(r => r.Date >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(r => r.Date <= endDate.Value);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(r => r.Type == type);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(r => r.Symptoms.Contains(keyword) || r.Recommendations.Contains(keyword));

            var totalCount = query.Count();

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new HealthRecordDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    Type = r.Type,
                    Symptoms = r.Symptoms,
                    Recommendations = r.Recommendations,
                    CustomFields = r.CustomFields,
                    Files = r.Files.Select(f => new HealthRecordFileDto
                    {
                        Id = f.Id,
                        HealthRecordId = f.HealthRecordId,
                        FileName = f.FileName,
                        FilePath = f.FilePath,
                        FileType = f.FileType
                    }).ToList()
                }).ToList();

            return (items, totalCount);
        }

        public async Task<HealthRecordDto> GetByIdAsync(Guid id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                throw new Exception("Health record not found");

            return new HealthRecordDto
            {
                Id = record.Id,
                Date = record.Date,
                Type = record.Type,
                Symptoms = record.Symptoms,
                Recommendations = record.Recommendations,
                CustomFields = record.CustomFields,
                Files = record.Files.Select(f => new HealthRecordFileDto
                {
                    Id = f.Id,
                    HealthRecordId = f.HealthRecordId,
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    FileType = f.FileType
                }).ToList()
            };
        }

        public async Task<HealthRecordDto> CreateAsync(HealthRecordDto dto)
        {
            if (string.IsNullOrEmpty(dto.Type))
                throw new ArgumentException("Type is required");

            var record = new HealthRecord
            {
                Id = Guid.NewGuid(),
                Date = dto.Date,
                Type = dto.Type,
                Symptoms = dto.Symptoms,
                Recommendations = dto.Recommendations,
                CustomFields = dto.CustomFields,
                UserId = Guid.Empty // Заглушка для авторизации
            };

            await _repository.AddAsync(record);
            return dto;
        }

        public async Task UpdateAsync(Guid id, HealthRecordDto dto)
        {
            if (string.IsNullOrEmpty(dto.Type))
                throw new ArgumentException("Type is required");

            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                throw new Exception("Health record not found");

            record.Date = dto.Date;
            record.Type = dto.Type;
            record.Symptoms = dto.Symptoms;
            record.Recommendations = dto.Recommendations;
            record.CustomFields = dto.CustomFields;

            await _repository.UpdateAsync(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                throw new Exception("Health record not found");

            await _repository.DeleteAsync(record);
        }
    }
}