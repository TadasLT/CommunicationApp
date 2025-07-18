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

        public Task<IEnumerable<Customer>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<int> AddAsync(Customer customer) => _repository.AddAsync(customer);
        public Task<bool> UpdateAsync(Customer customer) => _repository.UpdateAsync(customer);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
} 