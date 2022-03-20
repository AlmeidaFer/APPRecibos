
namespace Entidades.Models
{
    public interface IERecibos
    {
        string? comentario { get; set; }
        int? consecutivo { get; set; }
        DateTime? fecha { get; set; }
        int? id { get; set; }
        int? monedaId { get; set; }
        decimal? monto { get; set; }
        int? proveedorId { get; set; }
    }
}