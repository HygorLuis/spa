using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class ClienteRepository(IApplicationDbContext _dbContext) : BaseRepository<Cliente>(_dbContext), IClienteRepository
{ 
    public Cliente? BuscarPorCPF(string cpf) => _dbContext.Clientes.FirstOrDefault(c => c.CPF == cpf);
}