using Xunit;
using Moq;
using Domain.Interfaces.BLL;
using Domain.Models;
using CommunicationAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommunicationApp.Tests.CommunicationAPI
{
    public class CommunicationControllerTests
    {
        [Fact]
        public async Task SendMessage_ReturnsOk_WithCorrectMessage()
        {
            var customer = new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" };
            var template = new Template { Id = 2, Name = "Test", Subject = "Hello", Body = "Hello {0} ({1})" };
            var customerService = new Mock<ICustomerService>();
            var templateService = new Mock<ITemplateService>();
            customerService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);
            templateService.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(template);
            var controller = new CommunicationController(customerService.Object, templateService.Object);

            var result = await controller.SendMessage(1, 2) as OkObjectResult;

            Assert.NotNull(result);
            var json = System.Text.Json.JsonSerializer.Serialize(result.Value);
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Assert.NotNull(dict);
            Assert.Equal("john@example.com", dict["to"]);
            Assert.Equal("Hello", dict["subject"]);
            Assert.Equal("Hello John Doe (john@example.com)", dict["body"]);
        }
    }
}