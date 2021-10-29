using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class CategoriaService
    {
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
    }
}
