using AutoMapper;
using SPA.Application.Dtos;
using SPA.Domain.Entities;

namespace SPA.Application.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ReadProdutoDto>();
        CreateMap<CreateUpdateProdutoDto, Produto>();
    }
}