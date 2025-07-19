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

        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            try
            {
                var templates = await _repository.GetAllAsync();
                return templates;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error retrieving templates: {ex.Message}", ex);
            }
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            try
            {
                var template = await _repository.GetByIdAsync(id);
                return template;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error retrieving template with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Template template)
        {
            try
            {
                var id = await _repository.AddAsync(template);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error adding template: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Template template)
        {
            try
            {
                var updated = await _repository.UpdateAsync(template);
                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error updating template with ID {template.Id}: {ex.Message}", ex);
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
                throw new Exception($"Service error deleting template with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 