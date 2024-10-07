using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;

public class RecetaIngrediente
{
    [Key]
    public int Id { get; set; }  // Identificador único
    public int IdReceta { get; set; }
    public int IdIngrediente { get; set; }
    public float Cantidad { get; set; }


    public Receta Receta { get; set; } // Navegación hacia Receta

    public Ingrediente Ingrediente { get; set; } // Navegación hacia Ingrediente
}
