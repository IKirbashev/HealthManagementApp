using Microsoft.EntityFrameworkCore;
using HealthApp.Api.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Data.Repositories
{
    public class MedicationRepository : IRepository<Medication>
    {
        private readonly AppDbContext _context;

        public MedicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Medication>> GetAllAsync()
        {
            return await Task.FromResult(_context.Medications.Include(m => m.Intakes).AsQueryable());
        }

        public async Task<Medication> GetByIdAsync(Guid id)
        {
            return await _context.Medications.Include(m => m.Intakes).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Medication entity)
        {
            await _context.Medications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medication entity)
        {
            _context.Medications.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Medication entity)
        {
            _context.Medications.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}