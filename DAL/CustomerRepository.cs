using Domain.Models;
using Domain.Interfaces.DAL;
using System.Data;
using Dapper;

namespace DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var sql = "SELECT * FROM Customers";
            return await _dbConnection.QueryAsync<Customer>(sql);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Customers WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Customer customer)
        {
            var sql = @"INSERT INTO Customers (Name, Email) VALUES (@Name, @Email); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _dbConnection.QuerySingleAsync<int>(sql, customer);
            return id;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            var sql = "UPDATE Customers SET Name = @Name, Email = @Email WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, customer);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Customers WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
} 