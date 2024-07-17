using AutoMapper;
using Serilog;
using SPA.Application.Dtos;
using SPA.Application.Interfaces;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Application.Services;

public class ClienteAppService(IMapper _mapper, IClienteService _clienteService) : IClienteAppService
{
    public async Task<ServiceResult<IEnumerable<ReadClienteDto>>> BuscarClientesAsync()
    {
        try
        {
            var clientes = await _clienteService.BuscarClientesAsync();
            return ServiceResult<IEnumerable<ReadClienteDto>>.Success(_mapper.Map<IEnumerable<ReadClienteDto>>(clientes));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao buscar clientes: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao buscar clientes.");
        }
    }

    public async Task<ServiceResult<ReadClienteDto>> BuscarPorIdAsync(Guid idUsuario)
    {
        try
        {
            var cliente = await _clienteService.BuscarPorIdAsync(idUsuario);
            if (cliente == null)
                return ServiceResult<ReadClienteDto>.Failure("Cliente não encontrado!");

            return ServiceResult<ReadClienteDto>.Success(_mapper.Map<ReadClienteDto>(cliente));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao buscar cliente: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao buscar cliente.");
        }
    }

    public async Task<ServiceResult<ReadClienteDto>> CadastrarAsync(CreateUpdateClienteDto createClienteDto)
    {
        try
        {
            var cliente = _clienteService.BuscarPorCPF(createClienteDto.CPF);
            if (cliente != null)
                return ServiceResult<ReadClienteDto>.Failure($"Cliente já cadastrada como \"{cliente.Nome}\"!");

            cliente = _mapper.Map<Cliente>(createClienteDto);
            var clienteCadastrado = await _clienteService.CadastrarAsync(cliente);

            return ServiceResult<ReadClienteDto>.Success(_mapper.Map<ReadClienteDto>(clienteCadastrado));
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao cadastrar cliente: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao cadastrar cliente.");
        }
    }

    public async Task<ServiceResult<ReadClienteDto>> AtualizarAsync(Guid idCliente, CreateUpdateClienteDto updateClienteDto)
    {
        try
        {
            var cliente = await _clienteService.BuscarPorIdAsync(idCliente);
            if (cliente == null)
                return ServiceResult<ReadClienteDto>.Failure("Cliente não encontrado!");

            _mapper.Map(updateClienteDto, cliente);
            await _clienteService.AtualizarAsync(cliente);

            return ServiceResult<ReadClienteDto>.Success();
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao atualizar cliente: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao atualizar cliente.");
        }
    }

    public async Task<ServiceResult<ReadClienteDto>> ExcluirAsync(Guid idCliente)
    {
        try
        {
            var cliente = await _clienteService.BuscarPorIdAsync(idCliente);
            if (cliente == null)
                return ServiceResult<ReadClienteDto>.Failure("Cliente não encontrado!");

            await _clienteService.ExcluirAsync(cliente);
            return ServiceResult<ReadClienteDto>.Success();
        }
        catch (Exception ex)
        {
            Log.Error($"Erro ao excluir cliente: {ex.Message}\n{ex?.InnerException?.Message}\n{ex?.StackTrace}");
            throw new Exception("Erro ao excluir cliente.");
        }
    }
}