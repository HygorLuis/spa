﻿using AutoMapper;
using SPA.Application.Dtos;
using SPA.Domain.Entities;

namespace SPA.Application.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ReadClienteDto>();
        CreateMap<CreateUpdateClienteDto, Cliente>();
    }
}