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
            Models.Usuario u = new Models.Usuario();
            u.Login = dados.GetProperty("login").GetString();
            u.Senha = dados.GetProperty("senha").GetString();

            Services.UsuarioService us = new Services.UsuarioService();
            if(us.ValidaUsuario(u))
            {
                ViewBag.Msg = "BEM-VINDO";
                return Redirect("/Admin/Home/Index");
            }
            else
            {
                ViewBag.Msg = "Dados inválidos";
                return View("~/Views/Login/Index.cshtml");
            }
        }
    }
}
