using Xunit;
using Moq;
using Domain.Interfaces.BLL;
using Domain.Models;
using CommunicationAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunicationApp.Tests.CommunicationAPI
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOk_WithCustomers()
        {
            var customers = new List<Customer> { new Customer { Id = 1, Name = "A", Email = "a@a.com" } };
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);
            var controller = new CustomerController(service.Object);
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            var customer = new Customer { Id = 1, Name = "A", Email = "a@a.com" };
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);
            var controller = new CustomerController(service.Object);
            var result = await controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(1, value.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Customer)null);
            var controller = new CustomerController(service.Object);
            var result = await controller.GetById(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithId()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync(5);
            var controller = new CustomerController(service.Object);
            var customer = new Customer { Name = "A", Email = "a@a.com" };
            var result = await controller.Create(customer);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(5, createdResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdated()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(true);
            var controller = new CustomerController(service.Object);
            var customer = new Customer { Id = 1, Name = "A", Email = "a@a.com" };
            var result = await controller.Update(customer);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenNotUpdated()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(false);
            var controller = new CustomerController(service.Object);
            var customer = new Customer { Id = 1, Name = "A", Email = "a@a.com" };
            var result = await controller.Update(customer);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);
            var controller = new CustomerController(service.Object);
            var result = await controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenNotDeleted()
        {
            var service = new Mock<ICustomerService>();
            service.Setup(x => x.DeleteAsync(1)).ReturnsAsync(false);
            var controller = new CustomerController(service.Object);
            var result = await controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}