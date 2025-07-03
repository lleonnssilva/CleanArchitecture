using AutoMapper;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Mapper;
using CleanArchitecture.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            return services;
        }
        public static void AddApplicationMappingProfile(this IServiceCollection services)
        {
            MapperConfiguration mappingConfigApp = new(mc =>
            {
                mc.AddProfile(new ApplicationProfile());
            });

            services.AddSingleton(mappingConfigApp.CreateMapper());
        }

    }
}
