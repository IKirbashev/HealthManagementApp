using Microsoft.EntityFrameworkCore;
using HealthApp.Api.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Data.Repositories
{
    public class DocumentRepository : IRepository<Document>
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Document>> GetAllAsync()
        {
            return await Task.FromResult(_context.Documents.AsQueryable());
        }

        public async Task<Document> GetByIdAsync(Guid id)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Document entity)
        {
            await _context.Documents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document entity)
        {
            _context.Documents.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Document entity)
        {
            _context.Documents.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}