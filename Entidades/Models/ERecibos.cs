using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class ERecibos 
    {
        public int? id { get; set; }
        
        [Display(Name ="Consecutivo")]
        public int? consecutivo { get; set; }
        [Required(ErrorMessage ="Seleccione un proveedor")]
        [Display(Name = "Proveedor")]
        public int? proveedorId { get; set; }
        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "Seleccione una moneda")]
        public int? monedaId { get; set; }
        [Required(ErrorMessage = "Ingrese un monto")]
        [Display(Name = "Monto")]
        public decimal? monto { get; set; }
        [Required(ErrorMessage = "Seleccione una fecha")]
        [Display(Name = "Fecha")]
        public DateTime? fecha { get; set; }
        [Display(Name = "Comentario")]
        public string? comentario { get; set; }

    }
}
