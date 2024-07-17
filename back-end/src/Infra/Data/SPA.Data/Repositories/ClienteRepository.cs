using SPA.Data.Context;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Data.Repositories;

public class ClienteRepository(PostgresDbContext _postgresDbContext) : BaseRepository<Cliente>(_postgresDbContext), IClienteRepository
{ 
    public Cliente? BuscarPorCPF(string cpf) => _postgresDbContext.Clientes.FirstOrDefault(c => c.CPF == cpf);
}