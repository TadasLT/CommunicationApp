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
            var sql = "SELECT * FROM Templates";
            return await _dbConnection.QueryAsync<Template>(sql);
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Templates WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Template>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Template template)
        {
            var sql = @"INSERT INTO Templates (Name, Subject, Body) VALUES (@Name, @Subject, @Body); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _dbConnection.QuerySingleAsync<int>(sql, template);
            return id;
        }

        public async Task<bool> UpdateAsync(Template template)
        {
            var sql = "UPDATE Templates SET Name = @Name, Subject = @Subject, Body = @Body WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, template);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Templates WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
} 