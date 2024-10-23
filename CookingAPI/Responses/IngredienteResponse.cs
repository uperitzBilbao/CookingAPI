using System.ComponentModel.DataAnnotations;

namespace CookingAPI.Responses

{
    public class IngredienteResponse
    {
        [Required]
        public string Nombre { get; set; }

    }
}
