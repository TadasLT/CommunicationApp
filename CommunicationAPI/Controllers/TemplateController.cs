using Domain.Models;
using Domain.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _service;
        private readonly ILogger<TemplateController> _logger;

        public TemplateController(ITemplateService service, ILogger<TemplateController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Template>>> GetAll()
        {
            try
            {
                _logger.LogInformation("API: Retrieving all templates");
                var templates = await _service.GetAllAsync();
                _logger.LogInformation("API: Successfully retrieved {Count} templates", templates.Count());
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error retrieving templates");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("API: Retrieving template with ID {TemplateId}", id);
                var template = await _service.GetByIdAsync(id);
                if (template == null)
                {
                    _logger.LogWarning("API: Template with ID {TemplateId} not found", id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully retrieved template with ID {TemplateId}", id);
                return Ok(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error retrieving template with ID {TemplateId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Template template)
        {
            try
            {
                _logger.LogInformation("API: Creating new template with subject: {Subject}", template.Subject);
                var id = await _service.AddAsync(template);
                _logger.LogInformation("API: Successfully created template with ID {TemplateId}", id);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error creating template with subject: {Subject}", template.Subject);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] Template template)
        {
            try
            {
                _logger.LogInformation("API: Updating template with ID {TemplateId}", template.Id);
                var updated = await _service.UpdateAsync(template);
                if (!updated)
                {
                    _logger.LogWarning("API: Template with ID {TemplateId} not found for update", template.Id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully updated template with ID {TemplateId}", template.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error updating template with ID {TemplateId}", template.Id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("API: Deleting template with ID {TemplateId}", id);
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning("API: Template with ID {TemplateId} not found for deletion", id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully deleted template with ID {TemplateId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error deleting template with ID {TemplateId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 