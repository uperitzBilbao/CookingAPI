using System.ComponentModel.DataAnnotations;

namespace CookingAPI.Requests
{
    public class IngredienteRequest
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int IdTipoIngrediente { get; set; }

        public List<String> Alergenos { get; set; } = new List<String>();

    }
}
