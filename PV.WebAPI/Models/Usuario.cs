using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PV.WebAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Apellido { get; set; }

    public string? Contraseña { get; set; }


    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
    public string? CorreoElectronico { get; set; }

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
