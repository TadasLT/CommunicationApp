using Domain.Models;
using Domain.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplateController(ITemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Template>>> GetAll()
        {
            try
            {
                var templates = await _service.GetAllAsync();
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetById(int id)
        {
            try
            {
                var template = await _service.GetByIdAsync(id);
                if (template == null) return NotFound();
                return Ok(template);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Template template)
        {
            try
            {
                var id = await _service.AddAsync(template);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] Template template)
        {
            try
            {
                var updated = await _service.UpdateAsync(template);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 