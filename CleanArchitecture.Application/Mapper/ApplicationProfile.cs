using AutoMapper;
using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Cliente, ClienteDTO>()
               .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
               .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ReverseMap();

            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<Email, EmailDTO>().ReverseMap();
        }
    }

}
