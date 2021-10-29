using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class ProdutoService
    {
        public (bool, List<string>) ValidaProduto(Models.Produto produto)
        {
            bool ok = true;
            List<string> erros = new List<string>();
            decimal precoValida = produto.Preco, preco;
            int estoqueValida, estoque;

            return (ok, erros);
        }
    }
}
