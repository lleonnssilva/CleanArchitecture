using AutoMapper;
using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
namespace CleanArchitecture.Application.Mapping
{
    public class DomainToDTOMapping:Profile
    {
        public DomainToDTOMapping() 
        {

            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ReverseMap();

            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            //CreateMap<ContatoDTO, Contato>().ReverseMap();
        }
    }
}
