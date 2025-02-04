using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class VoucherViewModel
    {
        public string RpeCodpla { get; set; } = null!;
        public DateTime? RpeFechaIni { get; set; }
        public DateTime? RpeFechaFin { get; set; }
        public string? RpeCodexp { get; set; }
        public string? RpeNombreEmpleado { get; set; } = null!;
        public string? RpeCentroNombre { get; set; } = null!;
        public double? RpeSalario { get; set; } = null!;
        public double? RpePercepcion { get; set; }
        public int? RpeCodpue { get; set; }
        public string? RpePuesto { get; set; }
        public DateTime? RpeFechaIngreso { get; set; }
        public int? RpeNoRecibo { get; set; }
        public List<VoucherItem> Detalles { get; set; } = new List<VoucherItem>();
    }

    public class VoucherItem
    {
        public int? RpeCodtipo { get; set; }
        public string? RpeNombreTipo { get; set; }
        public double? RpeTiempo { get; set; }
        public string? RpeUnidadTiempo { get; set; }
        public string? MonedaPlanilla { get; set; }
        public double? RpePercepcion { get; set; }
        public string? RpeDescDeduccion { get; set; }
        public double? RpeValorDeduccion { get; set; }
        public double? RpeSaldoPrest { get; set; }
    }
}