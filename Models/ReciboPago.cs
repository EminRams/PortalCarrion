using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models;

public partial class ReciboPago
{
    public int RpeId { get; set; }

    public int? RpeCodcia { get; set; }

    [Display(Name = "Empresa")]
    public string? RpeNombreEmpresa { get; set; }

    [Display(Name = "Periodo")]
    public string? RpeCodpla { get; set; }

    public int? RpeCodtpl { get; set; }

    public DateTime? RpeFechaIni { get; set; }

    public DateTime? RpeFechaFin { get; set; }

    public int? RpeCodemp { get; set; }

    public string? RpeCodexp { get; set; }

    [Display(Name = "Empleado")]
    public string? RpeNombreEmpleado { get; set; }

    public int? RpeCodpue { get; set; }

    [Display(Name = "Puesto")]
    public string? RpePuesto { get; set; }

    public string? RpeUnidad { get; set; }

    public string? RpeNit { get; set; }

    public string? RpeIsss { get; set; }

    public string? RpeFormaPago { get; set; }

    public double? RpeSalarioHora { get; set; }

    public double? RpeSalario { get; set; }

    public int? RpeNoRecibo { get; set; }

    public string? RpeMoneda { get; set; }

    public int? RpeCodtipo { get; set; }

    public string? RpeNombreTipo { get; set; }

    public int? RpeTasa { get; set; }

    public double? RpeTiempo { get; set; }

    public string? RpeUnidadTiempo { get; set; }

    public double? RpePercepcion { get; set; }

    public double? RpeDeduccion { get; set; }

    public double? RpeSaldoPrest { get; set; }

    public string? RpeDescDeduccion { get; set; }

    public double? RpeValorDeduccion { get; set; }

    public double? RpeCodigoDeduccion { get; set; }

    public int? RpeOr { get; set; }

    public string? RpeTipo { get; set; }

    public string? RpeCentroNombre { get; set; }

    public string? Afp { get; set; }

    public string? RpeArea { get; set; }

    public string? RpeVacacionLeyenda { get; set; }

    public string? RpeVacacionLeyenda2 { get; set; }

    public string? RpeVacacionLeyenda3 { get; set; }

    public string? RpeUbicacion { get; set; }

    public string? RpeDistribucion { get; set; }

    public int? RpeOrden { get; set; }

    public int? RpeOrdenTdc { get; set; }

    public string? RpeDiasDesc { get; set; }

    public string? RpeDistribucionTotal { get; set; }

    [Display(Name = "Pago el")]
    public string? RpeFechaFinLetras { get; set; }

    public string? MonedaPlanilla { get; set; }

    public DateTime? RpeFechaIngreso { get; set; }

    public DateTime? Fecha { get; set; }

    [Display(Name = "Tipo")]
    public string? TplDescripcion { get; set; }
}
