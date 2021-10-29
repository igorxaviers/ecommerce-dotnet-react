using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Autenticar([FromBody] System.Text.Json.JsonElement dados)
        {
            bool ok = false;
            string mensagem = "";
            string url = "";
            
            Models.Usuario u = new Models.Usuario();
            u.Login = dados.GetProperty("login").GetString();
            u.Senha = dados.GetProperty("senha").GetString();

            (ok, mensagem) = u.ValidarLogin();
            if (ok)
            {
                Services.UsuarioService us = new Services.UsuarioService();
                (ok, mensagem) = us.ValidaUsuario(u);
                if (ok)
                {
                    url = "/Admin/Home";
                }
            }

            return Json(new {ok, mensagem, url});
        }
    }
}
