using Domain.Models;
using Domain.Interfaces.BLL;
using Domain.Interfaces.DAL;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all customers");
                var customers = await _repository.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} customers", customers.Count());
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers");
                throw new Exception($"Service error retrieving customers: {ex.Message}", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving customer with ID {CustomerId}", id);
                var customer = await _repository.GetByIdAsync(id);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved customer with ID {CustomerId}", id);
                }
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer with ID {CustomerId}", id);
                throw new Exception($"Service error retrieving customer with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation("Adding new customer with name: {Name}", customer.Name);
                var id = await _repository.AddAsync(customer);
                _logger.LogInformation("Successfully added customer with ID {CustomerId}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer with name: {Name}", customer.Name);
                throw new Exception($"Service error adding customer: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation("Updating customer with ID {CustomerId}", customer.Id);
                var updated = await _repository.UpdateAsync(customer);
                if (updated)
                {
                    _logger.LogInformation("Successfully updated customer with ID {CustomerId}", customer.Id);
                }
                else
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for update", customer.Id);
                }
                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with ID {CustomerId}", customer.Id);
                throw new Exception($"Service error updating customer with ID {customer.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting customer with ID {CustomerId}", id);
                var deleted = await _repository.DeleteAsync(id);
                if (deleted)
                {
                    _logger.LogInformation("Successfully deleted customer with ID {CustomerId}", id);
                }
                else
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for deletion", id);
                }
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with ID {CustomerId}", id);
                throw new Exception($"Service error deleting customer with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 