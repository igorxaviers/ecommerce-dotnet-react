using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoEcommerce.Areas.Admin.Models;
using ProjetoEcommerce.Areas.Admin.Services;

namespace ProjetoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Listagem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar([FromBody] System.Text.Json.JsonElement dados)
        {
            string mensagem = "";
            bool sucesso = true;
            decimal preco, precoValida;
            int estoque, estoqueValida;
            Models.Produto p = new Models.Produto();

            p.Id = dados.GetProperty("id").GetInt32();
            p.Nome = dados.GetProperty("nome").GetString();
            p.Categoria = new Categoria(dados.GetProperty("categoria_id").GetInt32(), "");
            p.Preco = dados.GetProperty("preco").GetDecimal();
            p.Estoque = dados.GetProperty("estoque").GetInt32();

            (sucesso, mensagem) = p.Validar();
            if(sucesso)
            {
                ProdutoService ps = new ProdutoService();
                sucesso = ps.Salvar(p);
                if(sucesso)
                    mensagem = "Produto cadastrado com sucesso";
                else
                    mensagem = "Erro ao salvar o produto";
            }


            return Json(new { mensagem, sucesso, p});
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            string mensagem = "";
            bool sucesso = true;
            ProdutoService ps = new ProdutoService();
            sucesso = ps.Excluir(id);
            if(sucesso)
                mensagem = "Produto excluído com sucesso";
            else
                mensagem = "Houve um erro ao excluir o produto";

            return Json(new { mensagem, sucesso });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            IEnumerable<Models.Produto> produtos;
            ProdutoService ps = new ProdutoService();
            produtos = ps.ListarProdutos();
            return Json(produtos);
        }
    }
}
