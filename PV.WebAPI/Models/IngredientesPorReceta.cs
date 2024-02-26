using System;
using System.Collections.Generic;

namespace PV.WebAPI.Models;

public partial class IngredientesPorReceta
{
    public int RelacionId { get; set; }

    public int? RecetaId { get; set; }

    public int? IngredienteId { get; set; }

    public decimal? Cantidad { get; set; }

    public string? UnidadMedida { get; set; }

    public virtual Ingrediente? Ingrediente { get; set; }

    public virtual Receta? Receta { get; set; }
}
