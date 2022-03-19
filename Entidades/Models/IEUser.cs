namespace Entidades.Models
{
    public interface IEUser
    {
        string email { get; set; }
        int? id { get; set; }
        string? name { get; set; }
        string pass { get; set; }
    }
}