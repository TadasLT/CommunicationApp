using Domain.Models;
using Domain.Interfaces.BLL;
using Domain.Interfaces.DAL;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _repository;
        private readonly ILogger<TemplateService> _logger;

        public TemplateService(ITemplateRepository repository, ILogger<TemplateService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all templates");
                var templates = await _repository.GetAllAsync();
                _logger.LogInformation("Successfully retrieved {Count} templates", templates.Count());
                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving templates");
                throw new Exception($"Service error retrieving templates: {ex.Message}", ex);
            }
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving template with ID {TemplateId}", id);
                var template = await _repository.GetByIdAsync(id);
                if (template == null)
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved template with ID {TemplateId}", id);
                }
                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving template with ID {TemplateId}", id);
                throw new Exception($"Service error retrieving template with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Template template)
        {
            try
            {
                _logger.LogInformation("Adding new template with subject: {Subject}", template.Subject);
                var id = await _repository.AddAsync(template);
                _logger.LogInformation("Successfully added template with ID {TemplateId}", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding template with subject: {Subject}", template.Subject);
                throw new Exception($"Service error adding template: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Template template)
        {
            try
            {
                _logger.LogInformation("Updating template with ID {TemplateId}", template.Id);
                var updated = await _repository.UpdateAsync(template);
                if (updated)
                {
                    _logger.LogInformation("Successfully updated template with ID {TemplateId}", template.Id);
                }
                else
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found for update", template.Id);
                }
                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating template with ID {TemplateId}", template.Id);
                throw new Exception($"Service error updating template with ID {template.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting template with ID {TemplateId}", id);
                var deleted = await _repository.DeleteAsync(id);
                if (deleted)
                {
                    _logger.LogInformation("Successfully deleted template with ID {TemplateId}", id);
                }
                else
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found for deletion", id);
                }
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting template with ID {TemplateId}", id);
                throw new Exception($"Service error deleting template with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 