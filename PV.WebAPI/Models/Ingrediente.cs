using System;
using System.Collections.Generic;

namespace PV.WebAPI.Models;

public partial class Ingrediente
{
    public int IngredienteId { get; set; }

    public string? NombreIngrediente { get; set; }

    public virtual ICollection<IngredientesPorReceta> IngredientesPorReceta { get; set; } = new List<IngredientesPorReceta>();
}
