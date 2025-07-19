using Domain.Models;
using Domain.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CommunicationController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ITemplateService _templateService;
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(ICustomerService customerService, ITemplateService templateService, ILogger<CommunicationController> logger)
        {
            _customerService = customerService;
            _templateService = templateService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromQuery] int customerId, [FromQuery] int templateId)
        {
            try
            {
                _logger.LogInformation("API: Sending message to customer {CustomerId} using template {TemplateId}", customerId, templateId);
                
                var customer = await _customerService.GetByIdAsync(customerId);
                if (customer == null)
                {
                    _logger.LogWarning("API: Customer {CustomerId} not found for message sending", customerId);
                    return NotFound($"Customer {customerId} not found");
                }
                
                var template = await _templateService.GetByIdAsync(templateId);
                if (template == null)
                {
                    _logger.LogWarning("API: Template {TemplateId} not found for message sending", templateId);
                    return NotFound($"Template {templateId} not found");
                }

                var message = string.Format(template.Body, customer.Name, customer.Email);
                _logger.LogInformation("API: Successfully formatted message for customer {CustomerId} using template {TemplateId}", customerId, templateId);
                
                Console.WriteLine($"Sending message to {customer.Email}: {message}");
                _logger.LogInformation("API: Message sent to {Email}: {Message}", customer.Email, message);

                return Ok(new { to = customer.Email, subject = template.Subject, body = message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error sending message to customer {CustomerId} using template {TemplateId}", customerId, templateId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 