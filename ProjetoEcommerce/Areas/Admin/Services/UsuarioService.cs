using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class UsuarioService
    {
        const string LOGIN_ADMIN = "admin";
        const string SENHA_ADMIN = "admin";
        public (bool, string) ValidaUsuario(Models.Usuario usuario)
        {
            if (usuario.Login == LOGIN_ADMIN && usuario.Senha == SENHA_ADMIN)
                return (true, "Usuário encontrado");
            else
                return (false, "Dados inválidos");
        }
    }
}
