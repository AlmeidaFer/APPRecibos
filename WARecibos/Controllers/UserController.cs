using Entidades.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WARecibos.Controllers
{
    public class UserController : Controller
    {
        private readonly string ApiUrl = "https://localhost:7150/User";
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Iniciar(EUser user)
        {
            if (ModelState.IsValid)
            {
                var data = new EUser();
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrl + "/Logear/" + user.email + "/" + user.pass);
                     data = JsonConvert.DeserializeObject<EUser>(result);

                    if (data!=null)
                    {
                        return RedirectToAction("Index", "Home", new { id = data.id});
                    }

                }

               
            }

            TempData["msj"] = "Verifique sus credenciales";
            return View("LogIn",user);
        }
    
    }
}
