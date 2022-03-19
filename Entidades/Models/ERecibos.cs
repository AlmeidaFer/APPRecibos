using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class ERecibos : IERecibos
    {
        public int? id { get; set; }

        [Display(Name = "Consecutivo")]
        public int? consecutivo { get; set; }

        [customSelect(ErrorMessage ="Seleccione un proveedor")]
        [Required(ErrorMessage = "Seleccione un proveedor")]
        [Display(Name = "Proveedor")]
        public int? proveedorId { get; set; }

        [Display(Name = "Moneda")]
        [customSelect(ErrorMessage = "Seleccione una moneda")]
        [Required(ErrorMessage = "Seleccione una moneda")]
        public int? monedaId { get; set; }
        [Range(0, 9999999999999999.9999,ErrorMessage ="Ingrese un monto valido")]
        [Required(ErrorMessage = "Ingrese un monto")]
        [Display(Name = "Monto")]
        public decimal? monto { get; set; }

        [Required(ErrorMessage = "Seleccione una fecha")]
        [Display(Name = "Fecha")]
        public DateTime? fecha { get; set; }

        [Display(Name = "Comentario")]
        public string? comentario { get; set; }

    }

    public class customSelect : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if ((int)value == 0)
            {
                return false;
            }
            return true;
        }
    }
}
