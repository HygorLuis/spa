using SPA.Domain.Entities;

namespace SPA.Domain.Interfaces;

public interface IClienteRepository : IBaseRepository<Cliente>
{
    Cliente? BuscarPorCPF(string cpf);
}