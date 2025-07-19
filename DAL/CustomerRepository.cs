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
            try
            {
                var sql = "SELECT * FROM Customers";
                return await _dbConnection.QueryAsync<Customer>(sql);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customers: {ex.Message}", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Customers WHERE Id = @Id";
                return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Customer customer)
        {
            try
            {
                var sql = @"INSERT INTO Customers (Name, Email) VALUES (@Name, @Email); SELECT CAST(SCOPE_IDENTITY() as int);";
                var id = await _dbConnection.QuerySingleAsync<int>(sql, customer);
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding customer: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                var sql = "UPDATE Customers SET Name = @Name, Email = @Email WHERE Id = @Id";
                var affected = await _dbConnection.ExecuteAsync(sql, customer);
                return affected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating customer with ID {customer.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sql = "DELETE FROM Customers WHERE Id = @Id";
                var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
                return affected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting customer with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 