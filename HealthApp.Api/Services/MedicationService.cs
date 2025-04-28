using HealthApp.Api.Data.Repositories;
using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IRepository<Medication> _repository;

        public MedicationService(IRepository<Medication> repository)
        {
            _repository = repository;
        }

        public async Task<(List<MedicationDto> Items, int TotalCount)> GetAllAsync(int page, int pageSize)
        {
            var query = await _repository.GetAllAsync();

            var totalCount = query.Count();

            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    DosageValue = m.DosageValue,
                    DosageUnit = m.DosageUnit,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IntakeTime = m.IntakeTime,
                    RepeatEveryNDays = m.RepeatEveryNDays,
                    Notes = m.Notes
                }).ToList();

            return (items, totalCount);
        }

        public async Task<MedicationDto> GetByIdAsync(Guid id)
        {
            var medication = await _repository.GetByIdAsync(id);
            if (medication == null)
                throw new Exception("Medication not found");

            return new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                DosageValue = medication.DosageValue,
                DosageUnit = medication.DosageUnit,
                StartDate = medication.StartDate,
                EndDate = medication.EndDate,
                IntakeTime = medication.IntakeTime,
                RepeatEveryNDays = medication.RepeatEveryNDays,
                Notes = medication.Notes
            };
        }

        public async Task<MedicationDto> CreateAsync(MedicationDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Name is required");
            if (dto.DosageValue <= 0)
                throw new ArgumentException("Dosage value must be positive");
            if (string.IsNullOrEmpty(dto.IntakeTime) || !TimeOnly.TryParse(dto.IntakeTime, out _))
                throw new ArgumentException("Intake time must be in HH:mm format (e.g., '08:00')");

            var medication = new Medication
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                DosageValue = dto.DosageValue,
                DosageUnit = dto.DosageUnit,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IntakeTime = dto.IntakeTime,
                RepeatEveryNDays = dto.RepeatEveryNDays > 0 ? dto.RepeatEveryNDays : 1,
                Notes = dto.Notes,
                UserId = Guid.Empty
            };

            await _repository.AddAsync(medication);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MedicationDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Name is required");
            if (dto.DosageValue <= 0)
                throw new ArgumentException("Dosage value must be positive");
            if (string.IsNullOrEmpty(dto.IntakeTime) || !TimeOnly.TryParse(dto.IntakeTime, out _))
                throw new ArgumentException("Intake time must be in HH:mm format (e.g., '08:00')");

            var medication = await _repository.GetByIdAsync(id);
            if (medication == null)
                throw new Exception("Medication not found");

            medication.Name = dto.Name;
            medication.DosageValue = dto.DosageValue;
            medication.DosageUnit = dto.DosageUnit;
            medication.StartDate = dto.StartDate;
            medication.EndDate = dto.EndDate;
            medication.IntakeTime = dto.IntakeTime;
            medication.RepeatEveryNDays = dto.RepeatEveryNDays > 0 ? dto.RepeatEveryNDays : 1;
            medication.Notes = dto.Notes;

            await _repository.UpdateAsync(medication);
        }

        public async Task DeleteAsync(Guid id)
        {
            var medication = await _repository.GetByIdAsync(id);
            if (medication == null)
                throw new Exception("Medication not found");

            await _repository.DeleteAsync(medication);
        }
    }
}