using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using SPA.Application.Dtos;
using SPA.Application.Services;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Test;

public class UsuarioAppServiceTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUsuarioService> _usuarioServiceMock;
    private readonly UsuarioAppService _usuarioAppService;

    public UsuarioAppServiceTest()
    {
        _usuarioServiceMock = new Mock<IUsuarioService>();
        _mapperMock = new Mock<IMapper>();

        _usuarioAppService = new UsuarioAppService(_mapperMock.Object, _usuarioServiceMock.Object);
    }

    [Fact(DisplayName = "Cadastrar usuário")]
    public async Task CadastrarUsuarioAsync()
    {
        //Arrange
        var createUsuarioDto = new CreateUsuarioDto
        {
            NomeCompleto = "Teste 1",
            Email = "teste@hotmail.com",
            Usuario = "teste1",
            Senha = "teste@123456",
            ConfirmarSenha = "teste@123456"
        };

        //Mock
        _mapperMock.Setup(m => m.Map<Usuario>(It.IsAny<CreateUsuarioDto>())).Returns<CreateUsuarioDto>(x => new Usuario
        {
            Name = x.NomeCompleto,
            Email = x.Email,
            UserName = x.Usuario
        });
        _mapperMock.Setup(m => m.Map<ReadUsuarioDto>(It.IsAny<Usuario>())).Returns<Usuario>(x => new ReadUsuarioDto
        {
            Id = Guid.NewGuid(),
            NomeCompleto = x.Name,
            Email = x.Email,
            Usuario = x.UserName
        });

        _usuarioServiceMock.Setup(u => u.CadastrarAsync(It.IsAny<Usuario>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        //Act
        var result = await _usuarioAppService.CadastrarAsync(createUsuarioDto);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id != Guid.Empty);
    }

    [Fact(DisplayName = "Buscar usuário por id")]
    public async Task BuscarPorIdAsync()
    {
        //Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Name = "Teste 1",
            Email = "teste@hotmail.com",
            UserName = "teste1"
        };

        //Mock
        _mapperMock.Setup(m => m.Map<ReadUsuarioDto>(It.IsAny<Usuario>())).Returns<Usuario>(x => new ReadUsuarioDto
        {
            Id = usuario.Id,
            NomeCompleto = usuario.Name,
            Email = usuario.Email,
            Usuario = usuario.UserName
        });

        _usuarioServiceMock.Setup(u => u.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(usuario);

        //Act
        var result = await _usuarioAppService.BuscarPorIdAsync(usuario.Id);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id != Guid.Empty);
    }
}