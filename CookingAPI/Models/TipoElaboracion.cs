using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;


public class TipoElaboracion
{
    [Key]
    public int IdTipoElaboracion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<Receta> Recetas { get; set; }
}

