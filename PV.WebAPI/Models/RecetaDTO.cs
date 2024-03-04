namespace PV.WebAPI.Models;

public partial class RecetaDTO : Receta
{
    public List<IngredientesPorReceta>? Ingredientes { get; set; }
}