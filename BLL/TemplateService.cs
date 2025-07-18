using Domain.Models;
using Domain.Interfaces.BLL;
using Domain.Interfaces.DAL;

namespace BLL
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _repository;

        public TemplateService(ITemplateRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Template>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Template?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<int> AddAsync(Template template) => _repository.AddAsync(template);
        public Task<bool> UpdateAsync(Template template) => _repository.UpdateAsync(template);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
} 