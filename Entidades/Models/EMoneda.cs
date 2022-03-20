using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class EMoneda : IEMoneda
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Ingrese una moneda")]
        public string? moneda { get; set; }
        [Required(ErrorMessage = "Ingrese una clave para la moneda")]
        public string? clave { get; set; }

    }
}
