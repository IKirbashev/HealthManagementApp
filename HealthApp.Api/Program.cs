using HealthApp.Api.Data;
using HealthApp.Api.Data.Repositories;
using HealthApp.Api.Models.Entities;
using HealthApp.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев
builder.Services.AddScoped<IRepository<HealthRecord>, HealthRecordRepository>();
builder.Services.AddScoped<IRepository<Biomarker>, BiomarkerRepository>();
builder.Services.AddScoped<IRepository<Medication>, MedicationRepository>();
builder.Services.AddScoped<IRepository<Document>, DocumentRepository>();

// Регистрация сервисов
builder.Services.AddScoped<IHealthRecordService, HealthRecordService>();
builder.Services.AddScoped<IBiomarkerService, BiomarkerService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();