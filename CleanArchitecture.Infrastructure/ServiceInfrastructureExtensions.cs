using AutoMapper;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Events;
using CleanArchitecture.Infrastructure.Identity.Data;
using CleanArchitecture.Infrastructure.Identity.Mapper;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Identity.Services;
using CleanArchitecture.Infrastructure.MessageBuss;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMessageProducer, MessageProducer>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("RedisConnection");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            MapperConfiguration mappingConfig = new(mc =>
            {
                mc.AddProfile(new InfrastructureIdentityProfile());
            });

            //services.AddSingleton(mappingConfig.CreateMapper());

        }
    }
}
