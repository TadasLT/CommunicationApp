using Domain.Models;
using Domain.Interfaces.DAL;
using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(IDbConnection connection, ILogger<CustomerRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all customers from database");
                var customers = await _connection.QueryAsync<Customer>("SELECT * FROM Customers");
                _logger.LogInformation("Successfully retrieved {Count} customers from database", customers.Count());
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers from database");
                throw new Exception($"Repository error retrieving customers: {ex.Message}", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving customer with ID {CustomerId} from database", id);
                var customer = await _connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customers WHERE Id = @Id", new { Id = id });
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found in database", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved customer with ID {CustomerId} from database", id);
                }
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer with ID {CustomerId} from database", id);
                throw new Exception($"Repository error retrieving customer with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation("Adding new customer with name: {Name} to database", customer.Name);
                var sql = "INSERT INTO Customers (Name, Email) VALUES (@Name, @Email); SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = await _connection.QuerySingleAsync<int>(sql, customer);
                _logger.LogInformation("Successfully added customer with ID {CustomerId} to database", id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer with name: {Name} to database", customer.Name);
                throw new Exception($"Repository error adding customer: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                _logger.LogInformation("Updating customer with ID {CustomerId} in database", customer.Id);
                var sql = "UPDATE Customers SET Name = @Name, Email = @Email WHERE Id = @Id";
                var rowsAffected = await _connection.ExecuteAsync(sql, customer);
                var updated = rowsAffected > 0;
                if (updated)
                {
                    _logger.LogInformation("Successfully updated customer with ID {CustomerId} in database", customer.Id);
                }
                else
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for update in database", customer.Id);
                }
                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with ID {CustomerId} in database", customer.Id);
                throw new Exception($"Repository error updating customer with ID {customer.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting customer with ID {CustomerId} from database", id);
                var sql = "DELETE FROM Customers WHERE Id = @Id";
                var rowsAffected = await _connection.ExecuteAsync(sql, new { Id = id });
                var deleted = rowsAffected > 0;
                if (deleted)
                {
                    _logger.LogInformation("Successfully deleted customer with ID {CustomerId} from database", id);
                }
                else
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for deletion in database", id);
                }
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with ID {CustomerId} from database", id);
                throw new Exception($"Repository error deleting customer with ID {id}: {ex.Message}", ex);
            }
        }
    }
} 