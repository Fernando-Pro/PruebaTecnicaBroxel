namespace Recetas.Dominio.Operacion;

[Table("Receta.db3")]
public class Receta
{
    [PrimaryKey, AutoIncrement]
    public int IdReceta { get; set; }
    public string NombreReceta { get; set; } = string.Empty;
    public string Ingredientes { get; set; } = string.Empty;
    public string Preparacion { get; set; } = string.Empty;
    public int TiempoPreparacion { get; set; }
    public EnumDificultad Dificultad { get; set; }
    public string ImagenSourceReceta { get; set; } = string.Empty;
    public bool Visible { get; set; }
}

