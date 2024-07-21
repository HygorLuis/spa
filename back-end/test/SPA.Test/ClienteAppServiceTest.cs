using AutoMapper;
using Moq;
using SPA.Application.Dtos;
using SPA.Application.Services;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Test;

public class ClienteAppServiceTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IClienteService> _clienteServiceMock;
    private readonly ClienteAppService _clienteAppService;

    public ClienteAppServiceTest()
    {
        _mapperMock = new Mock<IMapper>();
        _clienteServiceMock = new Mock<IClienteService>();

        _clienteAppService = new ClienteAppService(_mapperMock.Object, _clienteServiceMock.Object);
    }

    [Fact(DisplayName = "Buscar todos os clientes")]
    public async Task BuscarClientesAsync()
    {
        //Arrange

        //Mock
        _mapperMock.Setup(m => m.Map<IEnumerable<ReadClienteDto>>(It.IsAny<IEnumerable<Cliente>>())).Returns<IEnumerable<Cliente>>(x =>
            x.Select(dto => new ReadClienteDto
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                Endereco = dto.Endereco,
                Observacao = dto.Observacao
            })
        );

        _clienteServiceMock.Setup(p => p.BuscarClientesAsync()).ReturnsAsync([
            new Cliente {
                Id = Guid.NewGuid(),
                Nome = "Cliente 1",
                Email = "cliente1@hotmail.com",
                CPF = "11111111111",
                Telefone = "11111111111",
                Endereco = "Endereço do cliente 1",
                Observacao = "Observação do cliente 1"
            },
            new Cliente {
                Id = Guid.NewGuid(),
                Nome = "Cliente 2",
                Email = "cliente2@hotmail.com",
                CPF = "22222222222",
                Telefone = "22222222222",
                Endereco = "Endereço do cliente 2"
            }
        ]);

        //Act
        var result = await _clienteAppService.BuscarClientesAsync();

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Any());
    }

    [Fact(DisplayName = "Buscar cliente por id")]
    public async Task BuscarPorIdAsync()
    {
        //Arrange
        var idCliente = Guid.NewGuid();

        //Mock
        _mapperMock.Setup(m => m.Map<ReadClienteDto>(It.IsAny<Cliente>())).Returns<Cliente>(dto => new ReadClienteDto
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            CPF = dto.CPF,
            Telefone = dto.Telefone,
            Endereco = dto.Endereco,
            Observacao = dto.Observacao
        });

        _clienteServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Cliente
        {
            Id = idCliente,
            Nome = "Cliente 1",
            Email = "cliente1@hotmail.com",
            CPF = "11111111111",
            Telefone = "11111111111",
            Endereco = "Endereço do cliente 1",
            Observacao = "Observação do cliente 1"
        });

        //Act
        var result = await _clienteAppService.BuscarPorIdAsync(idCliente);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id == idCliente);
    }

    [Fact(DisplayName = "Cadastrar cliente")]
    public async Task CadastrarAsync()
    {
        //Arrange
        var createClienteDto = new CreateUpdateClienteDto
        {
            Nome = "Cliente 1",
            Email = "cliente1@hotmail.com",
            CPF = "11111111111",
            Telefone = "11111111111",
            Endereco = "Endereço do cliente 1",
            Observacao = "Observação do cliente 1"
        };

        //Mock
        _mapperMock.Setup(m => m.Map<Cliente>(It.IsAny<CreateUpdateClienteDto>())).Returns<CreateUpdateClienteDto>(x => new Cliente
        {
            Nome = x.Nome,
            Email = x.Email,
            CPF = x.CPF,
            Telefone = x.Telefone,
            Endereco = x.Endereco,
            Observacao = x.Observacao
        });
        _mapperMock.Setup(m => m.Map<ReadClienteDto>(It.IsAny<Cliente>())).Returns<Cliente>(dto => new ReadClienteDto
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            CPF = dto.CPF,
            Telefone = dto.Telefone,
            Endereco = dto.Endereco,
            Observacao = dto.Observacao
        });

        _clienteServiceMock.Setup(p => p.CadastrarAsync(It.IsAny<Cliente>())).ReturnsAsync(new Cliente
        {
            Id = Guid.NewGuid(),
            Nome = createClienteDto.Nome,
            CPF = createClienteDto.CPF,
            Email = createClienteDto.Email,
            Telefone = createClienteDto.Telefone,
            Endereco = createClienteDto.Endereco,
            Observacao = createClienteDto.Observacao
        });

        //Act
        var result = await _clienteAppService.CadastrarAsync(createClienteDto);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id != Guid.Empty);
    }

    [Fact(DisplayName = "Atualizar cliente")]
    public async Task AtualizarAsync()
    {
        //Arrange
        var idCliente = Guid.NewGuid();
        var updateClienteDto = new CreateUpdateClienteDto
        {
            Nome = "Cliente 1 Alteração",
            Email = "cliente1@hotmail.com",
            CPF = "11111111111",
            Telefone = "11111111111",
            Endereco = "Endereço do cliente 1",
            Observacao = "Observação do cliente 1"
        };

        //Mock
        _mapperMock.Setup(m => m.Map(It.IsAny<CreateUpdateClienteDto>(), It.IsAny<Cliente>())).Callback<CreateUpdateClienteDto, Cliente>((dto, cliente) =>
        {
            cliente.Nome = dto.Nome;
            cliente.CPF = dto.CPF;
            cliente.Email = dto.Email;
            cliente.Telefone = dto.Telefone;
            cliente.Endereco = dto.Endereco;
            cliente.Observacao = dto.Observacao;
        });

        _clienteServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Cliente
        {
            Id = idCliente,
            Nome = "Cliente 1",
            Email = "cliente1@hotmail.com",
            CPF = "11111111111",
            Telefone = "11111111111",
            Endereco = "Endereço do cliente 1",
            Observacao = "Observação do cliente 1"
        });

        //Act
        var result = await _clienteAppService.AtualizarAsync(idCliente, updateClienteDto);

        //Assert
        Assert.True(result.Succeeded);
    }

    [Fact(DisplayName = "Excluir cliente")]
    public async Task ExcluirAsync()
    {
        //Arrange
        var idCliente = Guid.NewGuid();

        //Mock
        _clienteServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Cliente
        {
            Id = idCliente,
            Nome = "Cliente 1",
            Email = "cliente1@hotmail.com",
            CPF = "11111111111",
            Telefone = "11111111111",
            Endereco = "Endereço do cliente 1",
            Observacao = "Observação do cliente 1"
        });

        //Act
        var result = await _clienteAppService.ExcluirAsync(idCliente);

        //Assert
        Assert.True(result.Succeeded);
    }
}