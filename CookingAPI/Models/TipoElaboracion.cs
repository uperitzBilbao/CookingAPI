using System.ComponentModel.DataAnnotations;


public class TipoElaboracion
{
    [Key]
    public int IdTipoElaboracion { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

