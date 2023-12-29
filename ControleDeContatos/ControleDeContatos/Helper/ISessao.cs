﻿using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoveSessaoDoUsuario();

        UsuarioModel GetSessaoDoUsuario();

    }
}
