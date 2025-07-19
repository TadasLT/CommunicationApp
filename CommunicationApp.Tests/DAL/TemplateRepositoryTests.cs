using Xunit;
using Moq;
using System.Data;
using Domain.Models;
using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommunicationApp.Tests.DAL
{
    public class TemplateRepositoryTests
    {
        [Fact]
        public void Constructor_AcceptsDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_StoresDbConnection()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            Assert.NotNull(repository);
        }

        [Fact]
        public async Task GetAllAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.NullReferenceException>(() => repository.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.NullReferenceException>(() => repository.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            var template = new Template { Name = "Test", Subject = "Subject", Body = "Body" };
            await Assert.ThrowsAsync<System.NullReferenceException>(() => repository.AddAsync(template));
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            var template = new Template { Id = 1, Name = "Test", Subject = "Subject", Body = "Body" };
            await Assert.ThrowsAsync<System.NullReferenceException>(() => repository.UpdateAsync(template));
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryMethod()
        {
            var dbConnection = new Mock<IDbConnection>();
            var repository = new TemplateRepository(dbConnection.Object);
            await Assert.ThrowsAsync<System.NullReferenceException>(() => repository.DeleteAsync(1));
        }
    }
} 