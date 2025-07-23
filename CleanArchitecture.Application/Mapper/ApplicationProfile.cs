using AutoMapper;
using CleanArchitecture.Application.DTOS.Cliente;
using CleanArchitecture.Application.DTOS.Email;
using CleanArchitecture.Application.DTOS.Endereco;
using CleanArchitecture.Application.DTOS.Telefone;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<Cliente, ClienteDTO>()
            //   //.ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
            //   .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco(src.Endereco.Rua, src.Endereco.Bairro, src.Endereco.Cidade, src.Endereco.Numero, src.Endereco.Estado, src.Endereco.Cep)))
            //   .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone))
            //   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //   .ReverseMap();
            CreateMap<ClienteDTO, Cliente>()
              .ConstructUsing(src => new Cliente(
                  src.Nome,
                  new Telefone(src.Telefone.DDD,src.Telefone.NumeroTelefone), // Exemplo: criando o objeto Telefone
                  new Email(src.Email.EnderecoEmail),       // Exemplo: criando o objeto Email
                  new Endereco(src.Endereco.Rua,src.Endereco.Bairro,src.Endereco.Cidade,src.Endereco.Numero,src.Endereco.Estado,src.Endereco.Cep)  // Exemplo: criando o objeto Endereco
              )).ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<Email, EmailDTO>().ReverseMap();
        }
    }

}
