using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;



public class TipoDieta
{
    [Key]
    public int IdTipoDieta { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public List<Receta> Recetas { get; set; }
}


