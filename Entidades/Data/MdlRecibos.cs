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
        public List<SelectList> proveedores { get; set; }
        public List<SelectList> monedas { get; set; }

        public MdlRecibos()
        {
            proveedores = new List<SelectList>();
            monedas = new List<SelectList>();
        }
    }
}
