using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;


public class TipoAlergeno
{
    [Key]
    public int IdTipoAlergeno { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public List<IngredienteAlergeno> IngredienteAlergenos { get; set; }
}


