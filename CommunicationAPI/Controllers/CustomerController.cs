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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService service, ILogger<CustomerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                _logger.LogInformation("API: Retrieving all customers");
                var customers = await _service.GetAllAsync();
                _logger.LogInformation("API: Successfully retrieved {Count} customers", customers.Count());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error retrieving customers");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("API: Retrieving customer with ID {CustomerId}", id);
                
                var customer = await _service.GetByIdAsync(id);
                if (customer == null)
                {
                    _logger.LogWarning("API: Customer with ID {CustomerId} not found", id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully retrieved customer with ID {CustomerId}", id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error retrieving customer with ID {CustomerId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Customer customer)
        {
            try
            {
                _logger.LogInformation("API: Creating new customer with name: {Name}", customer.Name);
                
                var id = await _service.AddAsync(customer);
                _logger.LogInformation("API: Successfully created customer with ID {CustomerId}", id);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error creating customer with name: {Name}", customer.Name);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            try
            {
                _logger.LogInformation("API: Updating customer with ID {CustomerId}", customer.Id);
                
                var updated = await _service.UpdateAsync(customer);
                if (!updated)
                {
                    _logger.LogWarning("API: Customer with ID {CustomerId} not found for update", customer.Id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully updated customer with ID {CustomerId}", customer.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error updating customer with ID {CustomerId}", customer.Id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("API: Deleting customer with ID {CustomerId}", id);
                
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning("API: Customer with ID {CustomerId} not found for deletion", id);
                    return NotFound();
                }
                _logger.LogInformation("API: Successfully deleted customer with ID {CustomerId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API: Error deleting customer with ID {CustomerId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 