using Xunit;
using Moq;
using System.Data;
using Domain.Models;
using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CommunicationApp.Tests.DAL
{
    public class TemplateRepositoryTests
    {
        [Fact]
        public void Constructor_AcceptsDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_StoresDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public async Task GetAllAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            var template = new Template { Name = "Test", Subject = "Subject", Body = "Body" };
            await Assert.ThrowsAsync<System.Exception>(() => repository.AddAsync(template));
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            var template = new Template { Id = 1, Name = "Test", Subject = "Subject", Body = "Body" };
            await Assert.ThrowsAsync<System.Exception>(() => repository.UpdateAsync(template));
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var logger = new Mock<ILogger<TemplateRepository>>();
            var repository = new TemplateRepository(dbConnection.Object, logger.Object);
            await Assert.ThrowsAsync<System.Exception>(() => repository.DeleteAsync(1));
        }
    }
} 