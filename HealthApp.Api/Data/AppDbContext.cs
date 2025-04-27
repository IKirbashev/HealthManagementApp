using Microsoft.EntityFrameworkCore;
using HealthApp.Api.Models.Entities;

namespace HealthApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<HealthRecordFile> HealthRecordFiles { get; set; }
        public DbSet<Biomarker> Biomarkers { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationIntake> MedicationIntakes { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Индексы для поиска
            modelBuilder.Entity<HealthRecord>()
                .HasIndex(r => r.Date);
            modelBuilder.Entity<HealthRecord>()
                .HasIndex(r => r.Type);

            modelBuilder.Entity<Biomarker>()
                .HasIndex(b => b.Date);
            modelBuilder.Entity<Biomarker>()
                .HasIndex(b => b.Name);

            modelBuilder.Entity<Medication>()
                .HasIndex(m => m.StartDate);

            modelBuilder.Entity<Document>()
                .HasIndex(d => d.UploadDate);
            modelBuilder.Entity<Document>()
                .HasIndex(d => d.Category);
        }
    }
}