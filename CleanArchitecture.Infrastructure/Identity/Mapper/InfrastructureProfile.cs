using AutoMapper;
using CleanArchitecture.Infrastructure.Identity.Models;
using Core.Application.DTOs;

namespace CleanArchitecture.Infrastructure.Identity.Mapper
{
    public class InfrastructureIdentityProfile : Profile
    {
        public InfrastructureIdentityProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ReverseMap();
        }
    }
}
