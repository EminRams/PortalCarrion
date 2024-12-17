using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models;

public partial class UsrUser
{
    [Display(Name = "Codigo")]
    public int UsrCodigo { get; set; }

    [Display(Name = "Identidad")]
    public string UsrUsername { get; set; } = null!;

    [Display(Name = "Nombre")]
    public string UsrNombreUsuario { get; set; } = null!;

    [Display(Name = "Activo")]
    public bool UsrActivo { get; set; }

    [Display(Name = "Modo de Autenticación")]
    public string UsrModoAutenticacion { get; set; } = null!;

    public DateTime? UsrUltimoAcceso { get; set; }

    [Display(Name = "Correo Electrónico")]
    public string? UsrEmail { get; set; }

    [Display(Name = "Contraseña")]
    public byte[]? UsrPassword { get; set; }

    public bool UsrPassVence { get; set; }

    public DateTime? UsrPassUltimoCambio { get; set; }

    public bool UsrPassCambiarProxAcceso { get; set; }

    public bool UsrVerMismo { get; set; }

    public bool UsrVerSubalternos { get; set; }

    public bool UsrVerSoloSubaltInmediat { get; set; }

    public string? UsrPropertyBagData { get; set; }

    public string? UsrEstadoWorkflow { get; set; }

    public string? UsrCodigoWorkflow { get; set; }

    public bool UsrIngresadoPortal { get; set; }

    public DateTime? UsrFechaCreacion { get; set; }

    public string? UsrUsuarioCreacion { get; set; }

    public DateTime? UsrFechaModificacion { get; set; }

    public string? UsrUsuarioModificacion { get; set; }

    public string? UsrTokenResetPwd { get; set; }

    public DateTime? UsrFechaExpToken { get; set; }

    public bool UsrUsuarioRoot { get; set; }

    public string? CambioPassPrimerinicio { get; set; }

    public string? LeerCondicionesPortal { get; set; }

    public string? LlenarInfoCovid { get; set; }

    public virtual ICollection<EusExpedienteUsuario> EusExpedienteUsuarios { get; set; } = new List<EusExpedienteUsuario>();
}
