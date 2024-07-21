using Microsoft.AspNetCore.Identity;
using Moq;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;
using SPA.Application.Services;
using SPA.Domain.Entities;

namespace SPA.Test;

public class AuthAppServiceTest
{
    private readonly Mock<IUserStore<Usuario>> _userStoreMock;
    private readonly Mock<UserManager<Usuario>> _userManagerMock;
    private readonly Mock<IJwtAppService> _jwtServiceMock;

    private readonly AuthAppService _authAppServiceService;

    public AuthAppServiceTest()
    {
        _userStoreMock = new Mock<IUserStore<Usuario>>();
        _userManagerMock = new Mock<UserManager<Usuario>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
        _jwtServiceMock = new Mock<IJwtAppService>();

        _authAppServiceService = new AuthAppService(_userManagerMock.Object, _jwtServiceMock.Object);
    }

    [Fact(DisplayName = "Autenticar usuário")]
    public async Task AutenticarAsync()
    {
        //Arrange
        var login = new LoginDto
        {
            Usuario = "teste1",
            Senha = "Teste1@1234"
        };

        //Mock
        _userManagerMock.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Usuario
        {
            Id = Guid.NewGuid(),
            Name = "Teste usuário 1",
            UserName = login.Usuario,
            Email = "teste@hotmail.com"
        });
        _userManagerMock.Setup(u => u.CheckPasswordAsync(It.IsAny<Usuario>(), It.IsAny<string>())).ReturnsAsync(true);

        _jwtServiceMock.Setup(t => t.GenerateJwtToken(It.IsAny<Usuario>())).Returns("aiufbiABFIHbfuyoebfuihwegrg4e6rh4eqr8g46qre4g864g6q4e8rg");

        //Act
        var result = await _authAppServiceService.AutenticarAsync(login);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(!string.IsNullOrWhiteSpace(result.Data));
    }
}