namespace Entidades.Models
{
    public interface IEMoneda
    {
        string? clave { get; set; }
        int? id { get; set; }
        string? moneda { get; set; }
    }
}