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
            string msg = "";
            string url = "";
            Boolean ok = true;
            string precoValida, estoqueValida;
            decimal preco;
            int estoque;
            Models.Produto p = new Models.Produto();

            p.Id = dados.GetProperty("id").GetInt32();
            p.Nome = dados.GetProperty("nome").GetString();
            precoValida = dados.GetProperty("preco").GetString();
            estoqueValida = dados.GetProperty("estoque").GetString();
            p.Categoria = dados.GetProperty("categoria").GetString();

            decimal.TryParse(precoValida, out preco);
            if (preco <= 0)
            {
                msg = "Preço inválido";
                ok = false;
            }
            else
                p.Preco = preco;

            int.TryParse(estoqueValida, out estoque);
            if (estoque <= 0)
            {
                msg = "Estoque inválido";
                ok = false;
            }
            else
                p.Estoque = estoque;

            if (String.IsNullOrEmpty(p.Nome))
            {
                msg = "Nome inválido";
                ok = false;
            }

            if (String.IsNullOrEmpty(p.Categoria))
            {
                msg = "Nenhuma categoria selecionada";
                ok = false;
            }

            if (ok)
            {
                url = "/Admin/Produto";
                msg = "Categoria cadastrada";
            }
            else
                url = "";

            return Json(new { msg, url, ok, p });
        }
    }
}
