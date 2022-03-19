using Entidades.Data;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WARecibos.Models;

namespace WARecibos.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {

            List<ERecibos> list = new List<ERecibos>();
            list.Add(new ERecibos {id=1, monto=1000M,consecutivo=1 });

            return View(list);
        }

        public IActionResult Create(int id = 0)
        {
            MdlRecibos data = new MdlRecibos();
            if (id!=0)
            {
                //buscar info
                data.id = 1;
                data.monto = 1000M;
                data.consecutivo = 1;
            }

            return View("Datos", data);
        }
        [HttpPost]
        public IActionResult IURecibo(MdlRecibos model)
        {
            if (ModelState.IsValid)
            {

            }

            return View("Datos", model);
        }


        public IActionResult Delete()
        {


            return View();
        }

    }
}