using Entidades.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WARecibos.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Iniciar(EUser user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("LogIn",user);
        }
    
    }
}
