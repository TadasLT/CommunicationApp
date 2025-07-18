using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.DAL;
using Domain.Interfaces.BLL;
using DAL;
using BLL;
using System.Data;

namespace CommunicationAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ITemplateService, TemplateService>();
            return services;
        }
    }
} 