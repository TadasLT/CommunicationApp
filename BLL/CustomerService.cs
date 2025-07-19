using Domain.Models;
using Domain.Interfaces.BLL;
using Domain.Interfaces.DAL;

namespace BLL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                var customers = await _repository.GetAllAsync();
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error retrieving customers: {ex.Message}", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id);
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error retrieving customer with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Customer customer)
        {
            try
            {
                var id = await _repository.AddAsync(customer);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error adding customer: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                var updated = await _repository.UpdateAsync(customer);
                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error updating customer with ID {customer.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error deleting customer with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 