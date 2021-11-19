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
            bool sucesso = false;
            string mensagem = "";
            string url = "";
            Models.Usuario u = new Models.Usuario();
            u.Login = dados.GetProperty("login").GetString();
            u.Senha = dados.GetProperty("senha").GetString();

            (sucesso, mensagem) = u.ValidarLogin();
            if(sucesso)
            {
                Services.UsuarioService us = new Services.UsuarioService();
                if (us.ValidarAutenticacao(u))
                {
                    mensagem = "Bem-vindo";
                    sucesso = true;
                    url = "Admin/Produto";
                }
                else
                {
                    mensagem = "Dados inválidos.";
                    sucesso = false;
                }
            }

            return Json(new { 
                mensagem,
                sucesso,
                url
            });
        }
    }
}
