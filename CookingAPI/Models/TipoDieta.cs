using System.ComponentModel.DataAnnotations;



public class TipoDieta
{
    [Key]
    public int IdTipoDieta { get; set; }
    public string Nombre { get; set; } = string.Empty;
}


