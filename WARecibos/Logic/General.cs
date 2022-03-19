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

    }
}
