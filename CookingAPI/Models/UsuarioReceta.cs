using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingAPI.Models
{
    public class UsuarioReceta
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("Receta")]
        public int? RecetaId { get; set; }
        public virtual Receta Receta { get; set; }
    }

}