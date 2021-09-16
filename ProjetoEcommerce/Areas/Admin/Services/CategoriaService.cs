using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class CategoriaService
    {
        public bool ValidaCategoria(Models.Categoria categoria)
        {
            return !String.IsNullOrEmpty(categoria.Nome);
        }
    }
}
