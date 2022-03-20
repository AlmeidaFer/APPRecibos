using ClosedXML.Excel;
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
        #region Propiedades
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
        #endregion

        #region Metodos

        #region Principal
        /// <summary>
        /// Accion para ir a la pagina principal que contiene el listado de todos los recibos
        /// </summary>
        /// <param name="id">id utilizado para simular identificacion del usuario</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("LogIn", "User");
            }

            var data = Gen.getDataPrincipal(ApiUrl,ApiUrlP,ApiUrlM);

            var list = data.Result;

            return View(list);
        }
        #endregion

        #region Create and Edit
        /// <summary>
        /// Accion para mandar a la ventana de creacion de recibo o de edicion segun corresponda
        /// </summary>
        /// <param name="id">id del recibo</param>
        /// <returns></returns>
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

            var dataT = Gen.getMonedasProveedores(data, ApiUrlP, ApiUrlM);

            data = dataT.Result;

            return View("Datos", data);
        }

        /// <summary>
        /// Accion para registrar o editar el recibo
        /// </summary>
        /// <param name="model">datos del recibo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> IURecibo(MdlRecibos model)
        {
            if (ModelState.IsValid)
            {
                var result = new HttpResponseMessage();
                using (var httpClt = new HttpClient())
                {
                    if (model.id == 0 || model.id == null)
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
                        //aqui optendriamos el usuario Id para poderlo devolver a la vista principal
                        int userId = 1;
                        return RedirectToAction("Index", new { id = userId });
                    }


                }

            }


            var dataT = Gen.getMonedasProveedores(model, ApiUrlP, ApiUrlM);

            model = dataT.Result;

            return View("Datos", model);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Ventana de confirmacion para eliminacion de recibo
        /// </summary>
        /// <param name="id">id del recibo</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var model = new MdlItemRecibo();


            using (var httpClt = new HttpClient())
            {
                var result = await httpClt.GetStringAsync(ApiUrl + "/" + id);

                model = JsonConvert.DeserializeObject<MdlItemRecibo>(result);
            }

            var data = Gen.getNameMonedaProveedor(model, ApiUrlP, ApiUrlM);
            model = data.Result;

            return View(model);
        }

        /// <summary>
        /// Accion para confirmar eliminacion de recibo
        /// </summary>
        /// <param name="id">id del recibo a eliminar</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteSi(int id)
        {
            var result = new HttpResponseMessage();
            using (var httpClt = new HttpClient())
            {

                result = await httpClt.DeleteAsync(ApiUrl + "/" + id);
                //aqui optendriamos el usuario Id para poderlo devolver a la vista principal
                int userId = 1;
                return RedirectToAction("Index", new { id = userId });
            

            }
        }
        #endregion

        public async Task<IActionResult> DownloadExcel()
        {
            var data = Gen.getDataPrincipal(ApiUrl, ApiUrlP, ApiUrlM);

            using (var file = new XLWorkbook())
            {
                var libro = file.AddWorksheet("Recibos");
                int r = 1,c =1;

                libro.Cell(r, c).Value = "Consecutivo";
                libro.Cell(r,c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c++;
                libro.Cell(r, c).Value = "Proveedor";
                libro.Cell(r, c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c++;
                libro.Cell(r, c).Value = "Monto";
                libro.Cell(r, c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c++;
                libro.Cell(r, c).Value = "Moneda";
                libro.Cell(r, c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c++;
                libro.Cell(r, c).Value = "Fecha";
                libro.Cell(r, c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c++;
                libro.Cell(r, c).Value = "Comentarios";
                libro.Cell(r, c).Style.Fill.BackgroundColor = XLColor.FromHtml("#142566");
                libro.Cell(r, c).Style.Font.FontColor = XLColor.White;
                c =1;
                r++;

                foreach (var d in data.Result)
                {
                    libro.Cell(r, c).Value = "RC-" + d.consecutivo.ToString().PadLeft(5, '0');
                    c++;
                    libro.Cell(r, c).Value = d.proveedor;
                    c++;
                    libro.Cell(r, c).Value = d.monto;
                    libro.Cell(r, c).Style.NumberFormat.Format = "_-* #,##0.00_-;-* #,##0.00_-;_-* \" - \"??_-;_-@_-"; 
                    c++;
                    libro.Cell(r, c).Value = d.moneda;
                    c++;
                    libro.Cell(r, c).Value = Convert.ToDateTime(d.fecha).ToShortDateString();
                    c++;
                    libro.Cell(r, c).Value = d.comentario;
                    c = 1;
                    r++;
                }

                libro.Column(1).Width = 15;
                libro.Column(2).Width = 25;
                libro.Column(3).Width = 15;
                libro.Column(4).Width = 15;
                libro.Column(5).Width = 20;
                libro.Column(6).Width = 30;

                using (var stream = new MemoryStream())
                {
                    file.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheettml.sheet", "recibos.xlsx");
                }
            }
          
               
        }

        #endregion
    }
}

