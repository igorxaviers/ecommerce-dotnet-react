using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] System.Text.Json.JsonElement dados)
        {
            Models.Categoria c = new Models.Categoria();

            c.Id = dados.GetProperty("id").GetInt32();
            c.Nome = dados.GetProperty("nome").GetString();

            string msg = "";
            string url = "";

            if (!String.IsNullOrEmpty(c.Nome))
            {
                url = "/Admin/Categoria/";
                msg = "Categoria cadastrada";
            }
            else
            {
                url = "/Admin/Categoria";
                msg = "Categoria não cadastrada, dados inválidos";
            }
            return Json(new { msg, url });
        }
    }
}
