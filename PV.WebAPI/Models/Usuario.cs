using System;
using System.Collections.Generic;

namespace PV.WebAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Apellido { get; set; }

    public string? Contraseña { get; set; }

    public string? CorreoElectronico { get; set; }

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
