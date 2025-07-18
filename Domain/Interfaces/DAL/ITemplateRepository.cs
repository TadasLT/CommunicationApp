using Domain.Models;

namespace Domain.Interfaces.DAL
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Template>> GetAllAsync();
        Task<Template?> GetByIdAsync(int id);
        Task<int> AddAsync(Template template);
        Task<bool> UpdateAsync(Template template);
        Task<bool> DeleteAsync(int id);
    }
} 