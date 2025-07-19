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
    public class TemplateServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsTemplatesFromRepository()
        {
            var templates = new List<Template> { new Template { Id = 1, Name = "Test", Subject = "Subject", Body = "Body" } };
            var repository = new Mock<ITemplateRepository>();
            repository.Setup(x => x.GetAllAsync()).ReturnsAsync(templates);
            var logger = new Mock<ILogger<TemplateService>>();
            var service = new TemplateService(repository.Object, logger.Object);

            var result = await service.GetAllAsync();

            Assert.Equal(templates, result);
            repository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTemplateFromRepository()
        {
            var template = new Template { Id = 1, Name = "Test", Subject = "Subject", Body = "Body" };
            var repository = new Mock<ITemplateRepository>();
            repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(template);
            var logger = new Mock<ILogger<TemplateService>>();
            var service = new TemplateService(repository.Object, logger.Object);

            var result = await service.GetByIdAsync(1);

            Assert.Equal(template, result);
            repository.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ReturnsIdFromRepository()
        {
            var template = new Template { Name = "Test", Subject = "Subject", Body = "Body" };
            var repository = new Mock<ITemplateRepository>();
            repository.Setup(x => x.AddAsync(template)).ReturnsAsync(5);
            var logger = new Mock<ILogger<TemplateService>>();
            var service = new TemplateService(repository.Object, logger.Object);

            var result = await service.AddAsync(template);

            Assert.Equal(5, result);
            repository.Verify(x => x.AddAsync(template), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsResultFromRepository()
        {
            var template = new Template { Id = 1, Name = "Test", Subject = "Subject", Body = "Body" };
            var repository = new Mock<ITemplateRepository>();
            repository.Setup(x => x.UpdateAsync(template)).ReturnsAsync(true);
            var logger = new Mock<ILogger<TemplateService>>();
            var service = new TemplateService(repository.Object, logger.Object);

            var result = await service.UpdateAsync(template);

            Assert.True(result);
            repository.Verify(x => x.UpdateAsync(template), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsResultFromRepository()
        {
            var repository = new Mock<ITemplateRepository>();
            repository.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);
            var logger = new Mock<ILogger<TemplateService>>();
            var service = new TemplateService(repository.Object, logger.Object);

            var result = await service.DeleteAsync(1);

            Assert.True(result);
            repository.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
} 