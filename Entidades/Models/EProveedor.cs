using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class EProveedor : IEProveedor
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Ingrese un proveedor")]
        public string? proveedor { get; set; }

    }
}
