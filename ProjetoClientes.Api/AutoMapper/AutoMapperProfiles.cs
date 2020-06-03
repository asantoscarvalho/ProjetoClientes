using AutoMapper;
using ProjetoClientes.Domain.Dto;
using ProjetoClientes.Domain.Entities;

namespace ProjetoClientes.Api.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
        }
    }
}