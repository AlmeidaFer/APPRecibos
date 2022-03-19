using Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entidades.Data
{
    
    public class MdlRecibos : ERecibos
    {
        public List<EProveedor> proveedores { get; set; }
        public List<EMoneda> monedas { get; set; }

        public MdlRecibos()
        {
            proveedores = new List<EProveedor>();
            monedas = new List<EMoneda>();
        }
    }

    public partial class MdlDeleteRecibos: ERecibos
    {
        public string proveedor { get; set; }
        public string moneda { get; set; }
    }
}
