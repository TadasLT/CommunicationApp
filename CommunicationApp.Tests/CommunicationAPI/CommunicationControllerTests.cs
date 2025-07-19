using Xunit;
using Moq;
using Domain.Interfaces.BLL;
using Domain.Models;
using CommunicationAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommunicationApp.Tests.CommunicationAPI
{
    public class CommunicationControllerTests
    {
        [Fact]
        public async Task SendMessage_ReturnsOk_WhenCustomerAndTemplateFound()
        {
            var customer = new Customer { Id = 1, Name = "John", Email = "john@test.com" };
            var template = new Template { Id = 1, Subject = "Test", Body = "Hello {0}, your email is {1}" };
            var customerService = new Mock<ICustomerService>();
            var templateService = new Mock<ITemplateService>();
            var logger = new Mock<ILogger<CommunicationController>>();
            
            customerService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);
            templateService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(template);
            
            var controller = new CommunicationController(customerService.Object, templateService.Object, logger.Object);
            var result = await controller.SendMessage(1, 1);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}