using Domain.Models;
using Domain.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Customer customer)
        {
            var id = await _service.AddAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            var updated = await _service.UpdateAsync(customer);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 