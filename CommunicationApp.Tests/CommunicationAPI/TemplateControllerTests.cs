using Xunit;
using Moq;
using Domain.Interfaces.BLL;
using Domain.Models;
using CommunicationAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommunicationApp.Tests.CommunicationAPI
{
    public class TemplateControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOk_WithTemplates()
        {
            var templates = new List<Template> { new Template { Id = 1, Name = "T", Subject = "S", Body = "B" } };
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(templates);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var result = await controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Template>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            var template = new Template { Id = 1, Name = "T", Subject = "S", Body = "B" };
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(template);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var result = await controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<Template>(okResult.Value);
            Assert.Equal(1, value.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Template)null);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var result = await controller.GetById(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithId()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.AddAsync(It.IsAny<Template>())).ReturnsAsync(5);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var template = new Template { Name = "T", Subject = "S", Body = "B" };
            var result = await controller.Create(template);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(5, createdResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdated()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.UpdateAsync(It.IsAny<Template>())).ReturnsAsync(true);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var template = new Template { Id = 1, Name = "T", Subject = "S", Body = "B" };
            var result = await controller.Update(template);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenNotUpdated()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.UpdateAsync(It.IsAny<Template>())).ReturnsAsync(false);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var template = new Template { Id = 1, Name = "T", Subject = "S", Body = "B" };
            var result = await controller.Update(template);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var result = await controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenNotDeleted()
        {
            var service = new Mock<ITemplateService>();
            service.Setup(x => x.DeleteAsync(1)).ReturnsAsync(false);
            var logger = new Mock<ILogger<TemplateController>>();
            var controller = new TemplateController(service.Object, logger.Object);
            var result = await controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}