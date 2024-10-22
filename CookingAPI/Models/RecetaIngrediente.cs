using CookingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RecetaIngrediente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? IdReceta { get; set; }
    public int? IdIngrediente { get; set; }
    public float Cantidad { get; set; }


    public virtual Receta? Receta { get; set; } // Navegación hacia Receta

    public virtual Ingrediente? Ingrediente { get; set; } // Navegación hacia Ingrediente
}
