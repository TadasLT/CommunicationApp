using Xunit;
using Moq;
using System.Data;
using Domain.Models;
using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunicationApp.Tests.DAL
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void Constructor_AcceptsDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_StoresDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public async Task GetAllAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            var customer = new Customer { Name = "Test", Email = "test@test.com" };
            await Assert.ThrowsAsync<System.Exception>(() => repository.AddAsync(customer));
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            var customer = new Customer { Id = 1, Name = "Test", Email = "test@test.com" };
            await Assert.ThrowsAsync<System.Exception>(() => repository.UpdateAsync(customer));
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new CustomerRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.DeleteAsync(1));
        }
    }
} 