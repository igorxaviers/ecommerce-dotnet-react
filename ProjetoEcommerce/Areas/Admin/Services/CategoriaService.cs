using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class CategoriaService
    {
        DAL.CategoriaDAL _cDAL = new DAL.CategoriaDAL();
        DAL.UsuarioDAL _uDAL = new DAL.UsuarioDAL();

        public (bool valido, List<string> erros) ValidaCategoria(Models.Categoria categoria)
        {
            bool valido = true;
            List<string> erros = new List<string>();
            if(String.IsNullOrEmpty(categoria.Nome))
            {
                erros.Add("Nome vazio");
                valido = false;
            }
            return (valido, erros);
        }
        
        public IEnumerable<Models.Categoria> Listar()
        {
            return _cDAL.Listar();
        }
        
        public bool Salvar(Models.Categoria c)
        {
            bool sucesso;
            string msg;
            
            (sucesso, msg) = c.Validar();
            if (sucesso)
            {
                sucesso = _cDAL.Salvar(c);
            }

            return sucesso;
        }
        
    }
}
