namespace PV.WebAPI.Models;

public partial class RecetaDTO{
    public Receta Receta { get; set; }
    public List<IngredientesPorReceta> IngredientesPorReceta  { get; set; }
}