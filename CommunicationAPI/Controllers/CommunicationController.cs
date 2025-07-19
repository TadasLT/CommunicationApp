using Domain.Models;
using Domain.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        public CommunicationController(ICustomerService customerService, ITemplateService templateService)
        {
            _customerService = customerService;
            _templateService = templateService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromQuery] int customerId, [FromQuery] int templateId)
        {
            var customer = await _customerService.GetByIdAsync(customerId);
            if (customer == null) return NotFound($"Customer {customerId} not found");
            var template = await _templateService.GetByIdAsync(templateId);
            if (template == null) return NotFound($"Template {templateId} not found");

            var message = string.Format(template.Body, customer.Name, customer.Email);
            Console.WriteLine($"Sending message to {customer.Email}: {message}");

            return Ok(new { to = customer.Email, subject = template.Subject, body = message });
        }
    }
} 