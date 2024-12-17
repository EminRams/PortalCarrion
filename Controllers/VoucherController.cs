using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalCarrion.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Rotativa.AspNetCore;
using PortalCarrion.Models.ViewModels;

namespace PortalCarrion.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private readonly DbA55028RecPagoCarrionContext _context;
        private readonly ICompositeViewEngine _viewEngine;
        public VoucherController(DbA55028RecPagoCarrionContext context, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _viewEngine = viewEngine;
        }

        // GET: Voucher
        public async Task<IActionResult> Index(string searchQuery, int pageSize = 10, int page = 1)
        {
            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrCodigo.ToString() == userClaim);

            var codigoEmpleado = await _context.EusExpedienteUsuarios
                .Where(e => e.EusCodusr == user!.UsrCodigo)
                .Select(e => e.CodigoEmp)
                .FirstOrDefaultAsync();

            // var voucherQuery = _context.ReciboPagos
            //     .Where(o => o.RpeCodtipo == 315 && o.RpeCodemp.ToString() == codigoEmpleado.ToString())
            //     .AsQueryable();

            var actualVouchers = _context.ReciboPagos
                .Where(o => o.RpeCodtipo == 315 && o.RpeCodemp.ToString() == codigoEmpleado.ToString())
                .ToList();

            var codigoExpediente = actualVouchers
                .Select(v => v.RpeCodexp)
                .FirstOrDefault();

            var voucherQuery = _context.ReciboPagos
                .Where(v => v.RpeCodexp == codigoExpediente)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                voucherQuery = voucherQuery.Where(o =>
                    o.RpeCodpla!.Contains(searchQuery) ||
                    o.RpeFechaFinLetras!.Contains(searchQuery) ||
                    o.TplDescripcion!.Contains(searchQuery) ||
                    o.RpeNombreEmpleado!.Contains(searchQuery) ||
                    o.RpeNombreEmpresa!.Contains(searchQuery) ||
                    o.RpePuesto!.Contains(searchQuery)
                );
            }

            var totalVouchers = await voucherQuery.CountAsync();

            var vouchers = await voucherQuery
                .OrderByDescending(o => o.RpeFechaFin)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable()
                .ToListAsync();

            ViewBag.SearchQuery = searchQuery;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalVouchers / (double)pageSize);
            
            return View(vouchers);
        }

        // GET: Voucher/Details/5
        public async Task<IActionResult> Details(int? id, bool download = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrCodigo.ToString() == userClaim);

            if (user == null)
            {
                return NotFound();
            }

            var codigoEmpleado = await _context.EusExpedienteUsuarios
                .Where(e => e.EusCodusr == user!.UsrCodigo)
                .Select(e => e.CodigoEmp)
                .FirstOrDefaultAsync();

            var reciboPago = await _context.ReciboPagos
                .Where(r => r.RpeCodemp.ToString() == codigoEmpleado.ToString())
                .FirstOrDefaultAsync(r => r.RpeId == id);

            var detalles = await _context.ReciboPagos
                .Where(r => r.RpeCodtipo != null || r.RpeDeduccion != null)
                .Where(r => r.RpeCodemp.ToString() == codigoEmpleado.ToString())
                .Where(r => r.RpeCodpla == reciboPago!.RpeCodpla)
                .ToListAsync();

            // Mapear los datos al ViewModel
            var detallesViewModel = detalles
           .Select(r => new VoucherItem
           {
               RpeCodtipo = r.RpeCodtipo,
               RpeNombreTipo = r.RpeNombreTipo,
               RpeTiempo = r.RpeTiempo,
               RpeUnidadTiempo = r.RpeUnidadTiempo,
               MonedaPlanilla = r.MonedaPlanilla,
               RpePercepcion = r.RpePercepcion,
               RpeDescDeduccion = r.RpeDescDeduccion,
               RpeValorDeduccion = r.RpeValorDeduccion,
               RpeSaldoPrest = r.RpeSaldoPrest
           })
           .ToList();

            VoucherViewModel voucher = new VoucherViewModel
            {
                RpeCodpla = reciboPago!.RpeCodpla!,
                RpeFechaIni = reciboPago.RpeFechaIni,
                RpeFechaFin = reciboPago.RpeFechaFin,
                RpeCodemp = reciboPago.RpeCodemp,
                RpeNombreEmpleado = reciboPago.RpeNombreEmpleado,
                RpeCentroNombre = reciboPago.RpeCentroNombre,
                RpeSalario = reciboPago.RpeSalario,
                RpePercepcion = reciboPago.RpePercepcion,
                RpeCodpue = reciboPago.RpeCodpue,
                RpePuesto = reciboPago.RpePuesto,
                RpeFechaIngreso = reciboPago.RpeFechaIngreso,
                RpeNoRecibo = reciboPago.RpeNoRecibo,
                Detalles = detallesViewModel,
            };

            // Generar PDF con Rotativa
            var voucherPdf = new ViewAsPdf("Voucher", voucher)
            {
                FileName = download ? $"Recibo_{id}.pdf" : null,
            };

            return voucherPdf;
        }
    }
}
