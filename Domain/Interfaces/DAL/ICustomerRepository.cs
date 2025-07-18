using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.DAL
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<int> AddAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
    }
} 