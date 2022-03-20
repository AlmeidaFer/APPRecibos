using Entidades.Data;
using Entidades.Models;
using Newtonsoft.Json;

namespace WARecibos.Logic
{
    public class General
    {
        public General()
        {

        }
        public async Task<MdlRecibos> getMonedasProveedores(MdlRecibos data,string ApiUrlP,string ApiUrlM)
        {
            var listProveedores = new List<EProveedor>();
            var listMonedas = new List<EMoneda>();

            using (var httpClt = new HttpClient())
            {
                var result = await httpClt.GetStringAsync(ApiUrlP);

                data.proveedores = JsonConvert.DeserializeObject<List<EProveedor>>(result);

            }

            using (var httpClt = new HttpClient())
            {
                var result = await httpClt.GetStringAsync(ApiUrlM);

                data.monedas = JsonConvert.DeserializeObject<List<EMoneda>>(result);
            }

            return data;

        }

        public async Task<MdlItemRecibo> getNameMonedaProveedor(MdlItemRecibo model, string ApiUrlP, string ApiUrlM)
        {
            var proveedor = new EProveedor();
            var moneda = new EMoneda();

            if (model.proveedorId != 0 || model.proveedorId !=null)
            {
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrlP + "/" + model.proveedorId);

                    proveedor = JsonConvert.DeserializeObject<EProveedor>(result);
                }
                model.proveedor = proveedor.proveedor;
            }

            if (model.monedaId != null || model.monedaId !=0)
            {
                using (var httpClt = new HttpClient())
                {
                    var result = await httpClt.GetStringAsync(ApiUrlM + "/" + model.monedaId);

                    moneda = JsonConvert.DeserializeObject<EMoneda>(result);
                }
                model.moneda = moneda.clave;
            }

            return model;

        }

    }
}
