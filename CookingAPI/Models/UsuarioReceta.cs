using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingAPI.Models
{
    public class UsuarioReceta
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Receta")]
        public int RecetaId { get; set; }
        public Receta Receta { get; set; }
    }

}