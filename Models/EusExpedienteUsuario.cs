using System;
using System.Collections.Generic;

namespace PortalCarrion.Models;

public partial class EusExpedienteUsuario
{
    public int CodigoEmp { get; set; }

    public int? EusCodusr { get; set; }

    public int? EusCodexp { get; set; }

    public virtual UsrUser? EusCodusrNavigation { get; set; }
}
