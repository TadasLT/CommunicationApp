using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.BLL
{
    public interface ITemplateService
    {
        Task<IEnumerable<Template>> GetAllAsync();
        Task<Template?> GetByIdAsync(int id);
        Task<int> AddAsync(Template template);
        Task<bool> UpdateAsync(Template template);
        Task<bool> DeleteAsync(int id);
    }
} 