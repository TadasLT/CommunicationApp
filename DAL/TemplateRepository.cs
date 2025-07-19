using Domain.Models;
using Domain.Interfaces.DAL;
using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<TemplateRepository> _logger;

        public TemplateRepository(IDbConnection connection, ILogger<TemplateRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all templates from database");
                var templates = await _connection.QueryAsync<Template>("SELECT * FROM Templates");
                _logger.LogInformation("Successfully retrieved {Count} templates from database", templates.Count());
                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving templates from database");
                throw new Exception($"Repository error retrieving templates: {ex.Message}", ex);
            }
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving template with ID {TemplateId} from database", id);
                var template = await _connection.QueryFirstOrDefaultAsync<Template>("SELECT * FROM Templates WHERE Id = @Id", new { Id = id });
                if (template == null)
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found in database", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved template with ID {TemplateId} from database", id);
                }
                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving template with ID {TemplateId} from database", id);
                throw new Exception($"Repository error retrieving template with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Template template)
        {
            try
            {
                _logger.LogInformation("Adding new template with subject: {Subject} to database", template.Subject);
                var sql = "INSERT INTO Templates (Name, Subject, Body) VALUES (@Name, @Subject, @Body); SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = await _connection.QuerySingleAsync<int>(sql, template);
                _logger.LogInformation("Successfully added template with ID {TemplateId} to database", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding template with subject: {Subject} to database", template.Subject);
                throw new Exception($"Repository error adding template: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Template template)
        {
            try
            {
                _logger.LogInformation("Updating template with ID {TemplateId} in database", template.Id);
                var sql = "UPDATE Templates SET Name = @Name, Subject = @Subject, Body = @Body WHERE Id = @Id";
                var rowsAffected = await _connection.ExecuteAsync(sql, template);
                var updated = rowsAffected > 0;
                if (updated)
                {
                    _logger.LogInformation("Successfully updated template with ID {TemplateId} in database", template.Id);
                }
                else
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found for update in database", template.Id);
                }
                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating template with ID {TemplateId} in database", template.Id);
                throw new Exception($"Repository error updating template with ID {template.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting template with ID {TemplateId} from database", id);
                var sql = "DELETE FROM Templates WHERE Id = @Id";
                var rowsAffected = await _connection.ExecuteAsync(sql, new { Id = id });
                var deleted = rowsAffected > 0;
                if (deleted)
                {
                    _logger.LogInformation("Successfully deleted template with ID {TemplateId} from database", id);
                }
                else
                {
                    _logger.LogWarning("Template with ID {TemplateId} not found for deletion in database", id);
                }
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting template with ID {TemplateId} from database", id);
                throw new Exception($"Repository error deleting template with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 