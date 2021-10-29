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
            bool ok;
            string mensagem;
            string nome = dados.GetProperty("nome").GetString();
            
            //Services.CategoriaService cs = new Services.CategoriaService();
            Models.Categoria c = new Models.Categoria(0, nome);
            
            (ok, mensagem) = c.Validar();
            if (ok)
            {
                mensagem = "Categoria cadastrada com sucesso"; 
            }
            return Json(new { ok, mensagem });
        }
    }
}
