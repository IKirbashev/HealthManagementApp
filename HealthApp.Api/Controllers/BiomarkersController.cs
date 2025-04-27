using HealthApp.Api.Models.Dtos;
using HealthApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiomarkersController : ControllerBase
    {
        private readonly IBiomarkerService _service;

        public BiomarkersController(IBiomarkerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string name)
        {
            var biomarkers = await _service.GetAllAsync(startDate, endDate, name);
            return Ok(biomarkers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var biomarker = await _service.GetByIdAsync(id);
            return Ok(biomarker);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BiomarkerDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BiomarkerDto dto)
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

        [HttpGet("trends")]
        public async Task<IActionResult> GetTrends([FromQuery] string name, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var trends = await _service.GetTrendsAsync(name, startDate, endDate);
            return Ok(trends);
        }
    }
}