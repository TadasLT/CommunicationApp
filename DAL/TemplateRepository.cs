using Domain.Models;
using Domain.Interfaces.DAL;
using System.Data;
using Dapper;

namespace DAL
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IDbConnection _dbConnection;

        public TemplateRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Templates";
                return await _dbConnection.QueryAsync<Template>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving templates: {ex.Message}", ex);
            }
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Templates WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<Template>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving template with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Template template)
        {
            try
            {
                var sql = @"INSERT INTO Templates (Name, Subject, Body) VALUES (@Name, @Subject, @Body); SELECT CAST(SCOPE_IDENTITY() as int);";
                var id = await _dbConnection.QuerySingleAsync<int>(sql, template);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding template: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Template template)
        {
            try
            {
                var sql = "UPDATE Templates SET Name = @Name, Subject = @Subject, Body = @Body WHERE Id = @Id";
                var affected = await _dbConnection.ExecuteAsync(sql, template);
                return affected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating template with ID {template.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sql = "DELETE FROM Templates WHERE Id = @Id";
                var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
                return affected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting template with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 