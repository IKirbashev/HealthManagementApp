using HealthApp.Api.Data.Repositories;
using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _repository;

        public DocumentService(IRepository<Document> repository)
        {
            _repository = repository;
        }

        public async Task<List<DocumentDto>> GetAllAsync(DateTime? startDate, DateTime? endDate, string category)
        {
            var query = await _repository.GetAllAsync();

            if (startDate.HasValue)
                query = query.Where(d => d.UploadDate >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(d => d.UploadDate <= endDate.Value);
            if (!string.IsNullOrEmpty(category))
                query = query.Where(d => d.Category == category);

            return query.Select(d => new DocumentDto
            {
                Id = d.Id,
                Name = d.Name,
                FilePath = d.FilePath,
                FileType = d.FileType,
                Category = d.Category,
                UploadDate = d.UploadDate
            }).ToList();
        }

        public async Task<DocumentDto> GetByIdAsync(Guid id)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null)
                throw new Exception("Document not found");

            return new DocumentDto
            {
                Id = document.Id,
                Name = document.Name,
                FilePath = document.FilePath,
                FileType = document.FileType,
                Category = document.Category,
                UploadDate = document.UploadDate
            };
        }

        public async Task<DocumentDto> CreateAsync(DocumentDto dto)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                FilePath = dto.FilePath,
                FileType = dto.FileType,
                Category = dto.Category,
                UploadDate = DateTime.UtcNow,
                UserId = Guid.Empty // Заглушка для авторизации
            };

            await _repository.AddAsync(document);
            return dto;
        }

        public async Task UpdateAsync(Guid id, DocumentDto dto)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null)
                throw new Exception("Document not found");

            document.Name = dto.Name;
            document.FilePath = dto.FilePath;
            document.FileType = dto.FileType;
            document.Category = dto.Category;
            document.UploadDate = dto.UploadDate;

            await _repository.UpdateAsync(document);
        }

        public async Task DeleteAsync(Guid id)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null)
                throw new Exception("Document not found");

            await _repository.DeleteAsync(document);
        }
    }
}