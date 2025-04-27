using Microsoft.EntityFrameworkCore;
using HealthApp.Api.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Api.Data.Repositories
{
    public class BiomarkerRepository : IRepository<Biomarker>
    {
        private readonly AppDbContext _context;

        public BiomarkerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Biomarker>> GetAllAsync()
        {
            return await Task.FromResult(_context.Biomarkers.AsQueryable());
        }

        public async Task<Biomarker> GetByIdAsync(Guid id)
        {
            return await _context.Biomarkers.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Biomarker entity)
        {
            await _context.Biomarkers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Biomarker entity)
        {
            _context.Biomarkers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Biomarker entity)
        {
            _context.Biomarkers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}