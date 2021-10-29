using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public (bool, string) ValidarLogin()
        {
            if (string.IsNullOrEmpty(Login))
                return (false, "Login é obrigatório");

            if (string.IsNullOrEmpty(Senha))
                return (false, "Senha é obrigatório");
            return (true, null);
        }
        public (bool, string) Validar() {
            if (string.IsNullOrEmpty(Nome))
                return (false, "Nome é obrigatório");

            if (string.IsNullOrEmpty(Login))
                return (false, "Login é obrigatório");

            if (string.IsNullOrEmpty(Senha))
                return (false, "Senha é obrigatório");

            return (true, null);
        }
    }
}
