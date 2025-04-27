using HealthApp.Api.Data.Repositories;
using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public class BiomarkerService : IBiomarkerService
    {
        private readonly IRepository<Biomarker> _repository;

        public BiomarkerService(IRepository<Biomarker> repository)
        {
            _repository = repository;
        }

        public async Task<List<BiomarkerDto>> GetAllAsync(DateTime? startDate, DateTime? endDate, string name)
        {
            var query = await _repository.GetAllAsync();

            if (startDate.HasValue)
                query = query.Where(b => b.Date >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(b => b.Date <= endDate.Value);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(b => b.Name == name);

            return query.Select(b => new BiomarkerDto
            {
                Id = b.Id,
                Name = b.Name,
                Date = b.Date,
                Value = b.Value,
                Unit = b.Unit,
                NormalRangeMin = b.NormalRangeMin,
                NormalRangeMax = b.NormalRangeMax,
                Comments = b.Comments
            }).ToList();
        }

        public async Task<BiomarkerDto> GetByIdAsync(Guid id)
        {
            var biomarker = await _repository.GetByIdAsync(id);
            if (biomarker == null)
                throw new Exception("Biomarker not found");

            return new BiomarkerDto
            {
                Id = biomarker.Id,
                Name = biomarker.Name,
                Date = biomarker.Date,
                Value = biomarker.Value,
                Unit = biomarker.Unit,
                NormalRangeMin = biomarker.NormalRangeMin,
                NormalRangeMax = biomarker.NormalRangeMax,
                Comments = biomarker.Comments
            };
        }

        public async Task<BiomarkerDto> CreateAsync(BiomarkerDto dto)
        {
            var biomarker = new Biomarker
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Date = dto.Date,
                Value = dto.Value,
                Unit = dto.Unit,
                NormalRangeMin = dto.NormalRangeMin,
                NormalRangeMax = dto.NormalRangeMax,
                Comments = dto.Comments,
                UserId = Guid.Empty // Заглушка для авторизации
            };

            await _repository.AddAsync(biomarker);
            return dto;
        }

        public async Task UpdateAsync(Guid id, BiomarkerDto dto)
        {
            var biomarker = await _repository.GetByIdAsync(id);
            if (biomarker == null)
                throw new Exception("Biomarker not found");

            biomarker.Name = dto.Name;
            biomarker.Date = dto.Date;
            biomarker.Value = dto.Value;
            biomarker.Unit = dto.Unit;
            biomarker.NormalRangeMin = dto.NormalRangeMin;
            biomarker.NormalRangeMax = dto.NormalRangeMax;
            biomarker.Comments = dto.Comments;

            await _repository.UpdateAsync(biomarker);
        }

        public async Task DeleteAsync(Guid id)
        {
            var biomarker = await _repository.GetByIdAsync(id);
            if (biomarker == null)
                throw new Exception("Biomarker not found");

            await _repository.DeleteAsync(biomarker);
        }

        public async Task<List<BiomarkerDto>> GetTrendsAsync(string name, DateTime startDate, DateTime endDate)
        {
            var query = await _repository.GetAllAsync();

            query = query.Where(b => b.Name == name && b.Date >= startDate && b.Date <= endDate);

            return query.OrderBy(b => b.Date)
                .Select(b => new BiomarkerDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Date = b.Date,
                    Value = b.Value,
                    Unit = b.Unit,
                    NormalRangeMin = b.NormalRangeMin,
                    NormalRangeMax = b.NormalRangeMax,
                    Comments = b.Comments
                }).ToList();
        }
    }
}