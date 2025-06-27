using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Events;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class ServiceInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IRabbitMQProducer, Events.RabbitMQProducer>();
           
            return services;
        }
    }
}
