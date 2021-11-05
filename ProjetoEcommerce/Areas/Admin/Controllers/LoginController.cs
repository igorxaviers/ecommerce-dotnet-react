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
            string msg = "";
            bool sucesso = false;
            if (us.ValidarAutenticacao(u))
            {
                msg = "Bem-vindo";
                sucesso = true;
            }
            else
            {
                msg = "Dados inválidos.";
            }

            return Json(new { 
                msg,
                sucesso
            });

        }
    }
}
