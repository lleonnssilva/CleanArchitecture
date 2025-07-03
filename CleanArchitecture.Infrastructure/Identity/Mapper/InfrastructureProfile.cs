using AutoMapper;
using CleanArchitecture.Application.DTOS.User;
using CleanArchitecture.Infrastructure.Identity.Models;

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
