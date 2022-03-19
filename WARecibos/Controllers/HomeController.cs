using Entidades.Data;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WARecibos.Logic;
using WARecibos.Models;

namespace WARecibos.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly string ApiUrl = "https://localhost:7150/Recibos";
        private readonly string ApiUrlM = "https://localhost:7150/Moneda";
        private readonly string ApiUrlP = "https://localhost:7150/Proveedor";

        private General _Gen;
        private General Gen
        {
            get
            {
                if (_Gen == null)
                {
                    _Gen = new General();
                }
                return _Gen;
            }
        }

        public async Task<IActionResult> Index(int id=0)
        {
            if (id==0)
            {
                return RedirectToAction("LogIn","User");
            }
            var list = new List<ERecibos>();
            using (var httpClt = new HttpClient())
            {
                var result = await httpClt.GetStringAsync(ApiUrl);

                list = JsonConvert.DeserializeObject<List<ERecibos>>(result);
            }

            return View(list);
        }

        public async Task<IActionResult> Create(int id = 0)
        {
            MdlRecibos data = new MdlRecibos();

            if (id != 0)
            {
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrl + "/" + id);

                    data = JsonConvert.DeserializeObject<MdlRecibos>(result);
                }
            }

            var dataT = Gen.getMonedasProveedores(data,ApiUrlP,ApiUrlM);

            data = dataT.Result;

            return View("Datos", data);
        }

        [HttpPost]
        public async Task<IActionResult> IURecibo(MdlRecibos model)
        {
            if (ModelState.IsValid)
            {
                var result = new HttpResponseMessage();
                using (var httpClt = new HttpClient())
                {
                    if (model.id == 0)
                    {
                        result = await httpClt.PostAsJsonAsync(ApiUrl, model);
                    }
                    else
                    {
                        result = await httpClt.PutAsJsonAsync(ApiUrl, model);
                    }

                    if (result.IsSuccessStatusCode)
                    {
                        if (model.id != 0)
                        {
                            var cons = await result.Content.ReadAsStringAsync();
                        }

                        return RedirectToAction("Index");
                    }


                }

            }


            var dataT = Gen.getMonedasProveedores(model, ApiUrlP, ApiUrlM);

            model = dataT.Result;

            return View("Datos", model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var model = new MdlDeleteRecibos();


            using (var httpClt = new HttpClient())
            {
                var result = await httpClt.GetStringAsync(ApiUrl + "/" + id);

                model = JsonConvert.DeserializeObject<MdlDeleteRecibos>(result);
            }

            var proveedor = new EProveedor();        
            var moneda = new EMoneda();

            if (proveedor != null)
            {
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrlP + "/" + model.proveedorId);

                    proveedor = JsonConvert.DeserializeObject<EProveedor>(result);
                }
                model.proveedor = proveedor.proveedor;
            }

            if (moneda != null)
            {
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrlM + "/" + model.monedaId);

                    moneda = JsonConvert.DeserializeObject<EMoneda>(result);
                }
                model.moneda = moneda.moneda;
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteSi(int id)
        {
            var result = new HttpResponseMessage();
            using (var httpClt = new HttpClient())
            {

                result = await httpClt.DeleteAsync(ApiUrl + "/" + id);
                return RedirectToAction("Index");

            }
        }

    }
}

