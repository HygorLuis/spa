using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Domain.Services;

public class ClienteService(IClienteRepository _clienteRepository) : IClienteService
{
    public async Task<IEnumerable<Cliente>> BuscarClientesAsync() => await _clienteRepository.GetAllAsync();

    public async Task<Cliente?> BuscarPorIdAsync(Guid idCliente) => await _clienteRepository.FindByIdAsync(idCliente);

    public Cliente? BuscarPorCPF(string cpf) => _clienteRepository.BuscarPorCPF(cpf);

    public async Task<Cliente> CadastrarAsync(Cliente cliente)
    {
        await _clienteRepository.AddAsync(cliente);
        await _clienteRepository.SaveChangesAsync();

        return cliente;
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        await _clienteRepository.UpdateAsync(cliente);
        await _clienteRepository.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Cliente cliente)
    {
        _clienteRepository.Delete(cliente);
        await _clienteRepository.SaveChangesAsync();
    }
}