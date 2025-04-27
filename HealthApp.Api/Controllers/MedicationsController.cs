using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IMedicationService _service;

        public MedicationsController(IMedicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medications = await _service.GetAllAsync();
            return Ok(medications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var medication = await _service.GetByIdAsync(id);
            return Ok(medication);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicationDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicationDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/intakes")]
        public async Task<IActionResult> RecordIntake(Guid id, [FromBody] MedicationIntakeDto dto)
        {
            var intake = await _service.RecordIntakeAsync(id, dto);
            return CreatedAtAction(nameof(GetById), new { id = intake.Id }, intake);
        }
    }
}