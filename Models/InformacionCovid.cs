using System;
using System.Collections.Generic;

namespace PortalCarrion.Models;

public partial class InformacionCovid
{
    public int CodEmpleado { get; set; }

    public string? Identidad { get; set; }

    public int? Edad { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Embarazo { get; set; }

    public string? EnfermedadBase { get; set; }

    public DateOnly? FechaRegistro { get; set; }
}
