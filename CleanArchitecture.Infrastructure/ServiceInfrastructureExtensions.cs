using AutoMapper;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IMessageSQSProducer, MessageSQSProducer>();
            //services.AddScoped<IMessageRMQProducer, MessageRMQProducer>();
            

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("RedisConnection");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<AuthService>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                //o.Audience = "api";
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes((AuthService.JWT_SECURIRY_KEY)))
                };
            });
            MapperConfiguration mappingConfig = new(mc =>
            {
                mc.AddProfile(new InfrastructureIdentityProfile());
            });

            //services.AddSingleton(mappingConfig.CreateMapper());

        }
    }
}
