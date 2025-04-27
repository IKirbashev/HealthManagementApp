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

        public async Task<List<MedicationDto>> GetAllAsync()
        {
            var query = await _repository.GetAllAsync();

            return query.Select(m => new MedicationDto
            {
                Id = m.Id,
                Name = m.Name,
                DosageValue = m.DosageValue,
                DosageUnit = m.DosageUnit,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                IntakeTimes = m.IntakeTimes,
                RepeatEveryNDays = m.RepeatEveryNDays,
                Notes = m.Notes,
                Intakes = m.Intakes.Select(i => new MedicationIntakeDto
                {
                    Id = i.Id,
                    MedicationId = i.MedicationId,
                    IntakeTime = i.IntakeTime,
                    Status = i.Status,
                    Reason = i.Reason
                }).ToList()
            }).ToList();
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
                IntakeTimes = medication.IntakeTimes,
                RepeatEveryNDays = medication.RepeatEveryNDays,
                Notes = medication.Notes,
                Intakes = medication.Intakes.Select(i => new MedicationIntakeDto
                {
                    Id = i.Id,
                    MedicationId = i.MedicationId,
                    IntakeTime = i.IntakeTime,
                    Status = i.Status,
                    Reason = i.Reason
                }).ToList()
            };
        }

        public async Task<MedicationDto> CreateAsync(MedicationDto dto)
        {
            var medication = new Medication
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                DosageValue = dto.DosageValue,
                DosageUnit = dto.DosageUnit,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IntakeTimes = dto.IntakeTimes,
                RepeatEveryNDays = dto.RepeatEveryNDays,
                Notes = dto.Notes,
                UserId = Guid.Empty // Заглушка для авторизации
            };

            await _repository.AddAsync(medication);
            return dto;
        }

        public async Task UpdateAsync(Guid id, MedicationDto dto)
        {
            var medication = await _repository.GetByIdAsync(id);
            if (medication == null)
                throw new Exception("Medication not found");

            medication.Name = dto.Name;
            medication.DosageValue = dto.DosageValue;
            medication.DosageUnit = dto.DosageUnit;
            medication.StartDate = dto.StartDate;
            medication.EndDate = dto.EndDate;
            medication.IntakeTimes = dto.IntakeTimes;
            medication.RepeatEveryNDays = dto.RepeatEveryNDays;
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

        public async Task<MedicationIntakeDto> RecordIntakeAsync(Guid medicationId, MedicationIntakeDto dto)
        {
            var medication = await _repository.GetByIdAsync(medicationId);
            if (medication == null)
                throw new Exception("Medication not found");

            var intake = new MedicationIntake
            {
                Id = Guid.NewGuid(),
                MedicationId = medicationId,
                IntakeTime = dto.IntakeTime,
                Status = dto.Status,
                Reason = dto.Reason
            };

            medication.Intakes.Add(intake);
            await _repository.UpdateAsync(medication);
            return dto;
        }
    }
}