﻿using Microsoft.AspNetCore.Identity;
using SPA.Domain.Entities;
using SPA.Domain.Interfaces;

namespace SPA.Domain.Services;

public class UsuarioService(IUsuarioRepository _usuarioRepository) : IUsuarioService
{
    public async Task<IdentityResult> CadastrarAsync(Usuario usuario, string senha)
    {
        usuario.RegistrationDate = DateTime.UtcNow;
        return await _usuarioRepository.AddAsync(usuario, senha);
    }

    public async Task<Usuario?> BuscarPorIdAsync(Guid idUsuario) => await _usuarioRepository.FindByIdAsync(idUsuario);
}