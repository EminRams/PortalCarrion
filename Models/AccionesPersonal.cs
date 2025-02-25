using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models;

public partial class AccionesPersonal
{
    public int AcpId { get; set; }

    [Display(Name = "Fecha de Creación")]
    public DateTime AcpFechaCreacion { get; set; }

    [Display(Name = "Código de Empleado")]
    public string AcpCodexpEmpleado { get; set; } = null!;

    [Display(Name = "Nombre Completo")]
    public string? AcpNombreEmpleado { get; set; }

    [Display(Name = "Compañía")]
    public string? AcpCompaniaEmpleado { get; set; }

    [Display(Name = "Tienda")]
    public string? AcpTiendaEmpleado { get; set; }

    [Display(Name = "Departamento")]
    public string? AcpDepartamentoEmpleado { get; set; }

    [Display(Name = "Puesto")]
    public string? AcpPuestoEmpleado { get; set; }

    [Display(Name = "Condición")]
    public string? AcpCondicionEmpleado { get; set; }

    [Display(Name = "Salario")]
    public string? AcpSalarioEmpleado { get; set; }

    [Display(Name = "Fecha de Ingreso")]
    public DateTime? AcpFechaIngresoEmpleado { get; set; }

    [Display(Name = "Razón")]
    public string? AcpRazon { get; set; }

    [Display(Name = "Motivo")]
    public string? AcpMotivo { get; set; }

    [Display(Name = "Nuevo Salario")]
    public double? AcpNuevoSalario { get; set; }

    [Display(Name = "Nueva Tienda")]
    public string? AcpNuevaTienda { get; set; }

    [Display(Name = "Nuevo Departamento")]
    public string? AcpNuevoDepartamento { get; set; }

    [Display(Name = "Nuevo Puesto")]
    public string? AcpNuevoPuesto { get; set; }

    [Display(Name = "Nueva Condición")]
    public string? AcpNuevaCondicion { get; set; }

    [Display(Name = "Estado")]
    public string? AcpEstado { get; set; }

    [Display(Name = "Rige a Partir de la Fecha")]
    public DateTime? AcpFechaRigePartir { get; set; }

    [Display(Name = "Rige Hasta la Fecha")]
    public DateTime? AcpFechaRigeHasta { get; set; }
}
