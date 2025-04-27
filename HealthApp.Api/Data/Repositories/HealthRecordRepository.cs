using Microsoft.EntityFrameworkCore;
using HealthApp.Api.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Data.Repositories
{
    public class HealthRecordRepository : IRepository<HealthRecord>
    {
        private readonly AppDbContext _context;

        public HealthRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<HealthRecord>> GetAllAsync()
        {
            return await Task.FromResult(_context.HealthRecords.Include(r => r.Files).AsQueryable());
        }

        public async Task<HealthRecord> GetByIdAsync(Guid id)
        {
            return await _context.HealthRecords.Include(r => r.Files).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(HealthRecord entity)
        {
            await _context.HealthRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HealthRecord entity)
        {
            _context.HealthRecords.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(HealthRecord entity)
        {
            _context.HealthRecords.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}