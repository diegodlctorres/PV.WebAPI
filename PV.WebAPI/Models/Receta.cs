using System;
using System.Collections.Generic;

namespace PV.WebAPI.Models;

public partial class Receta
{
    public int RecetaId { get; set; }

    public string? NombreReceta { get; set; }

    public string? InstruccionesPreparacion { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<IngredientesPorReceta> IngredientesPorReceta { get; set; } = new List<IngredientesPorReceta>();

    public virtual Usuario? Usuario { get; set; }
}
