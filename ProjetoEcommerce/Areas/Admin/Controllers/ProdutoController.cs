using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] System.Text.Json.JsonElement dados)
        {
            string mensagem = "";
            Boolean ok = true;
            string precoValida, estoqueValida;
            decimal preco;
            int estoque;
            string url = "";
            Models.Produto p = new Models.Produto();
            // Services.ProdutoService ps = new Services.ProdutoService();

            p.Id = dados.GetProperty("id").GetInt32();
            p.Nome = dados.GetProperty("nome").GetString();
            p.Categoria = dados.GetProperty("categoria").GetString();
            precoValida = dados.GetProperty("preco").GetString();
            estoqueValida = dados.GetProperty("estoque").GetString();

            decimal.TryParse(precoValida, out preco);
            p.Preco = preco;

            int.TryParse(estoqueValida, out estoque);
            p.Estoque = estoque;

            (ok, mensagem) = p.Validar();

            if (ok)
            {
                mensagem = "Produto cadastrado com sucesso";
                url = "/Admin/Produto";
            }
            else
                url = "";

            return Json(new { mensagem, url, ok, p });
        }
    }
}
