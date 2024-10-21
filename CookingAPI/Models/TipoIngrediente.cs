using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;


public class TipoIngrediente
{
    [Key]
    public int IdTipoIngrediente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public List<Ingrediente> Ingredientes { get; set; }
}

