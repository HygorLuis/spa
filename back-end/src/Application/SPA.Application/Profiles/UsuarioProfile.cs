using AutoMapper;
using SPA.Application.Dtos;
using SPA.Domain.Entities;

namespace SPA.Application.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>()
            .ForMember(user => user.Name, opt => opt.MapFrom(dto => dto.NomeCompleto))
            .ForMember(user => user.UserName, opt => opt.MapFrom(dto => dto.Usuario));

        CreateMap<Usuario, ReadUsuarioDto>()
            .ForMember(dto => dto.NomeCompleto, opt => opt.MapFrom(user => user.Name))
            .ForMember(dto => dto.Usuario, opt => opt.MapFrom(user => user.UserName));
    }
}