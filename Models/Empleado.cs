using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models;

public partial class Empleado
{
    public int EmpId { get; set; }

    [Display(Name = "Código de Empleado")]
    public string? EmpCodigoAlternativo { get; set; }

    [Display(Name = "Código de Usuario")]
    public int? EmpCodigoUsuario { get; set; }

    [Display(Name = "Nombre Completo")]
    public string? EmpNombre { get; set; }

    [Display(Name = "Fecha de Ingreso")]
    public DateTime? EmpFechaIngreso { get; set; }

    [Display(Name = "Compañia")]
    public string? EmpCompania { get; set; }

    [Display(Name = "Tienda")]
    public string? EmpTienda { get; set; }

    [Display(Name = "Departamento")]
    public string? EmpDepartamento { get; set; }

    [Display(Name = "Puesto")]
    public string? EmpPuesto { get; set; }

    [Display(Name = "Condición")]
    public string? EmpCondicion { get; set; }

    [Display(Name = "Salario")]
    public double? EmpSalario { get; set; }
}
