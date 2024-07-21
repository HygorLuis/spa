using AutoMapper;
using Moq;
using SPA.Application.Dtos;
using SPA.Application.Services;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Test;

public class ProdutoAppServiceTest
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IProdutoService> _produtoServiceMock;
    private readonly ProdutoAppService _produtoAppService;

    public ProdutoAppServiceTest()
    {
        _mapperMock = new Mock<IMapper>();
        _produtoServiceMock = new Mock<IProdutoService>();

        _produtoAppService = new ProdutoAppService(_mapperMock.Object, _produtoServiceMock.Object);
    }

    [Fact(DisplayName = "Buscar todos os produtos")]
    public async Task BuscarProdutosAsync()
    {
        //Arrange

        //Mock
        _mapperMock.Setup(m => m.Map<IEnumerable<ReadProdutoDto>>(It.IsAny<IEnumerable<Produto>>())).Returns<IEnumerable<Produto>>(x =>
            x.Select(dto => new ReadProdutoDto
            {
                Id = dto.Id,
                Nome = dto.Nome,
                QuantidadeEstoque = dto.QuantidadeEstoque,
                ValorCusto = dto.ValorCusto,
                ValorVenda = dto.ValorVenda,
                Observacao = dto.Observacao,
                DataCadastro = dto.DataCadastro
            })
        );

        _produtoServiceMock.Setup(p => p.BuscarProdutosAsync()).ReturnsAsync([
            new Produto {
                Id = Guid.NewGuid(),
                Nome = "Produto 1",
                QuantidadeEstoque = 4,
                ValorCusto = 10,
                ValorVenda = 15,
                Observacao = "Teste observação",
                DataCadastro = new DateTime(2024, 07, 21, 12, 10 , 00)
            },
            new Produto {
                Id = Guid.NewGuid(),
                Nome = "Produto 2",
                QuantidadeEstoque = 28,
                ValorCusto = 16,
                ValorVenda = 30,
                DataCadastro = new DateTime(2024, 07, 21, 12, 15 , 00)
            },
            new Produto {
                Id = Guid.NewGuid(),
                Nome = "Produto 3",
                QuantidadeEstoque = 0,
                ValorCusto = 28,
                ValorVenda = 30,
                Observacao = "Teste observação 3",
                DataCadastro = new DateTime(2024, 07, 21, 12, 20 , 00)
            },
        ]);

        //Act
        var result = await _produtoAppService.BuscarProdutosAsync();

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Any());
    }

    [Fact(DisplayName = "Buscar produto por id")]
    public async Task BuscarPorIdAsync()
    {
        //Arrange
        var idProduto = Guid.NewGuid();

        //Mock
        _mapperMock.Setup(m => m.Map<ReadProdutoDto>(It.IsAny<Produto>())).Returns<Produto>(x => new ReadProdutoDto
        {
            Id = x.Id,
            Nome = x.Nome,
            QuantidadeEstoque = x.QuantidadeEstoque,
            ValorCusto = x.ValorCusto,
            ValorVenda = x.ValorVenda,
            Observacao = x.Observacao,
            DataCadastro = x.DataCadastro
        });

        _produtoServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Produto
        {
            Id = idProduto,
            Nome = "Produto 1",
            QuantidadeEstoque = 4,
            ValorCusto = 10,
            ValorVenda = 15,
            Observacao = "Teste observação",
            DataCadastro = new DateTime(2024, 07, 21, 12, 10, 00)
        });

        //Act
        var result = await _produtoAppService.BuscarPorIdAsync(idProduto);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id == idProduto);
    }

    [Fact(DisplayName = "Cadastrar produto")]
    public async Task CadastrarAsync()
    {
        //Arrange
        var createProdutoDto = new CreateUpdateProdutoDto
        {
            Nome = "Produto 1",
            QuantidadeEstoque = 4,
            ValorCusto = 10,
            ValorVenda = 15,
            Observacao = "Teste observação"
        };

        //Mock
        _mapperMock.Setup(m => m.Map<Produto>(It.IsAny<CreateUpdateProdutoDto>())).Returns<CreateUpdateProdutoDto>(x => new Produto
        {
            Nome = x.Nome,
            QuantidadeEstoque = x.QuantidadeEstoque,
            ValorCusto = x.ValorCusto,
            ValorVenda = x.ValorVenda,
            Observacao = x.Observacao
        });
        _mapperMock.Setup(m => m.Map<ReadProdutoDto>(It.IsAny<Produto>())).Returns<Produto>(x => new ReadProdutoDto
        {
            Id = x.Id,
            Nome = x.Nome,
            QuantidadeEstoque = x.QuantidadeEstoque,
            ValorCusto = x.ValorCusto,
            ValorVenda = x.ValorVenda,
            Observacao = x.Observacao,
            DataCadastro = x.DataCadastro
        });

        _produtoServiceMock.Setup(p => p.CadastrarAsync(It.IsAny<Produto>())).ReturnsAsync(new Produto
        {
            Id = Guid.NewGuid(),
            Nome = createProdutoDto.Nome,
            QuantidadeEstoque = createProdutoDto.QuantidadeEstoque,
            ValorCusto = createProdutoDto.ValorCusto,
            ValorVenda = createProdutoDto.ValorVenda,
            Observacao = createProdutoDto.Observacao,
            DataCadastro = new DateTime(2024, 07, 21, 12, 10, 00)
        });

        //Act
        var result = await _produtoAppService.CadastrarAsync(createProdutoDto);

        //Assert
        Assert.True(result.Succeeded);
        Assert.True(result.Data.Id != Guid.Empty);
    }

    [Fact(DisplayName = "Atualizar produto")]
    public async Task AtualizarAsync()
    {
        //Arrange
        var idProduto = Guid.NewGuid();
        var updateProdutoDto = new CreateUpdateProdutoDto
        {
            Nome = "Produto 1 Alteração",
            QuantidadeEstoque = 50,
            ValorCusto = 10,
            ValorVenda = 15,
            Observacao = "Teste observação"
        };

        //Mock
        _mapperMock.Setup(m => m.Map(It.IsAny<CreateUpdateProdutoDto>(), It.IsAny<Produto>())).Callback<CreateUpdateProdutoDto, Produto>((dto, produto) =>
        {
            produto.Nome = dto.Nome;
            produto.QuantidadeEstoque = dto.QuantidadeEstoque;
            produto.ValorCusto = dto.ValorCusto;
            produto.ValorVenda = dto.ValorVenda;
            produto.Observacao = dto.Observacao;
        });

        _produtoServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Produto
        {
            Id = idProduto,
            Nome = "Produto 1",
            QuantidadeEstoque = 4,
            ValorCusto = 10,
            ValorVenda = 15,
            Observacao = "Teste observação",
            DataCadastro = new DateTime(2024, 07, 21, 12, 10, 00)
        });

        //Act
        var result = await _produtoAppService.AtualizarAsync(idProduto, updateProdutoDto);

        //Assert
        Assert.True(result.Succeeded);
    }

    [Fact(DisplayName = "Excluir produto")]
    public async Task ExcluirAsync()
    {
        //Arrange
        var idProduto = Guid.NewGuid();

        //Mock
        _produtoServiceMock.Setup(p => p.BuscarPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Produto
        {
            Id = idProduto,
            Nome = "Produto 1",
            QuantidadeEstoque = 4,
            ValorCusto = 10,
            ValorVenda = 15,
            Observacao = "Teste observação",
            DataCadastro = new DateTime(2024, 07, 21, 12, 10, 00)
        });

        //Act
        var result = await _produtoAppService.ExcluirAsync(idProduto);

        //Assert
        Assert.True(result.Succeeded);
    }
}