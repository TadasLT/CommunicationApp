using Xunit;
using Moq;
using Domain.Interfaces.DAL;
using Domain.Models;
using BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CommunicationApp.Tests.BLL
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsCustomersFromRepository()
        {
            var customers = new List<Customer> { new Customer { Id = 1, Name = "John", Email = "john@test.com" } };
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository.Object, logger.Object);

            var result = await service.GetAllAsync();

            Assert.Equal(customers, result);
            repository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCustomerFromRepository()
        {
            var customer = new Customer { Id = 1, Name = "John", Email = "john@test.com" };
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository.Object, logger.Object);

            var result = await service.GetByIdAsync(1);

            Assert.Equal(customer, result);
            repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsIdFromRepository()
        {
            var customer = new Customer { Name = "John", Email = "john@test.com" };
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.AddAsync(customer)).ReturnsAsync(5);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository.Object, logger.Object);

            var result = await service.AddAsync(customer);

            Assert.Equal(5, result);
            repository.Verify(x => x.AddAsync(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsResultFromRepository()
        {
            var customer = new Customer { Id = 1, Name = "John", Email = "john@test.com" };
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.UpdateAsync(customer)).ReturnsAsync(true);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository.Object, logger.Object);

            var result = await service.UpdateAsync(customer);

            Assert.True(result);
            repository.Verify(x => x.UpdateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsResultFromRepository()
        {
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);
            var logger = new Mock<ILogger<CustomerService>>();
            var service = new CustomerService(repository.Object, logger.Object);

            var result = await service.DeleteAsync(1);

            Assert.True(result);
            repository.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
} 