using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> BuscarClientesAsync();
    Task<Cliente?> BuscarPorIdAsync(Guid idCliente);
    Task<Cliente> CadastrarAsync(Cliente cliente);
    Cliente? BuscarPorCPF(string cpf);
    Task AtualizarAsync(Cliente cliente);
    Task ExcluirAsync(Cliente cliente);
}