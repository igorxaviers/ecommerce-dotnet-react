using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] System.Text.Json.JsonElement dados)
        {
            Models.Produto p = new Models.Produto();

            p.Id = dados.GetProperty("id").GetInt32();
            p.Nome = dados.GetProperty("nome").GetString();
            p.Preco = dados.GetProperty("preco").GetDouble();
            p.Estoque = dados.GetProperty("estoque").GetInt32();
            p.Categoria = dados.GetProperty("categoria").GetString();

            string msg = "";
            string url = "";

            Services.CategoriaService cs = new Services.CategoriaService();
            if (cs.ValidaCategoria(p))
            {
                url = "/Admin/Categoria/";
                msg = "Categoria cadastrada";
                return Json(new { msg, url });
            }
            else
            {
                url = "/Admin/Categoria";
                msg = "Categoria não cadastrada, dados inválidos";
                return Json(new { msg, url });
            }
        }
    }
}
