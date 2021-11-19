using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class UsuarioService
    {
        DAL.UsuarioDAL _uDAL = new DAL.UsuarioDAL();
        
        public bool Salvar(Models.Usuario u)
        {
            bool sucesso;
            string msg;
            (sucesso, msg) = u.Validar();

            if (sucesso)
            {
                sucesso = false;
                var usuarioAchado = _uDAL.Consulta(u.Nome).FirstOrDefault();

                if (usuarioAchado != null && usuarioAchado.Id != u.Id)
                {
                    msg = "Nome existente.";
                }
                else
                {
                    sucesso = _uDAL.Salvar(u);
                }
            }

            return sucesso;
        }



        //public bool ValidarAutenticacao(string nome, string senha)
        //{
        //    if (nome == "admin" && senha == "123")
        //        return true;

        //    return false;
        //}

        public IEnumerable<Models.Usuario> Consulta(string nome)
        {
            //List<Models.Usuario> usuarios = _uDAL.Consulta(nome).ToList();
            return _uDAL.Consulta(nome);
        }


        public Models.Usuario Obter(int id)
        {
            return _uDAL.Obter(id);
        }

        public bool ValidarAutenticacao(Models.Usuario usuario)
        {
            return _uDAL.ValidarAutenticacao(usuario.Login, usuario.Senha);
        }
    }
}
