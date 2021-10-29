using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploForm.Controllers
{
    [Area("Admin")]
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
