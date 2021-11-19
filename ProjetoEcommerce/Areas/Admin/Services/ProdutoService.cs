using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Services
{
    public class ProdutoService
    {
        DAL.ProdutoDAL _pDAL = new DAL.ProdutoDAL();

        public (bool, List<string>) ValidaProduto(Models.Produto produto)
        {
            bool ok = true;
            List<string> erros = new List<string>();
            decimal precoValida = produto.Preco, preco;
            int estoqueValida, estoque;

            return (ok, erros);
        }
        
        //listar todos os produtos
        public IEnumerable<Models.Produto> ListarProdutos()
        {
            return _pDAL.Consulta("");
        }

        public bool Salvar(Models.Produto p)
        {
            return _pDAL.Salvar(p);
        }

        public bool Excluir(int id)
        {
            return _pDAL.Excluir(id);
        }
        
    }
}
