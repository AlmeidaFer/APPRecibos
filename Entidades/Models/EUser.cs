using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Models
{
    public class EUser 
    {
        public int? id { get; set; }
        [Required(ErrorMessage ="Ingrese un correo")]
        [EmailAddress(ErrorMessage ="Verifique su correo")]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Ingrese una contrasñe")]
        public string pass { get; set; }
        public string? name { get; set; }

    }
}
