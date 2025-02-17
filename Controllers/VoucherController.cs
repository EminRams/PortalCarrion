using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalCarrion.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Rotativa.AspNetCore;
using PortalCarrion.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace PortalCarrion.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private readonly DbA55028RecPagoCarrionContext _context;
        private readonly ICompositeViewEngine _viewEngine;

        private readonly IConverter _converter;
        public VoucherController(DbA55028RecPagoCarrionContext context, ICompositeViewEngine viewEngine, IConverter converter)
        {
            _context = context;
            _viewEngine = viewEngine;
            _converter = converter;
        }

        // GET: Voucher
        public async Task<IActionResult> Index(string searchQuery)
        {
            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrCodigo.ToString() == userClaim);

            var salaryTypes = new List<string> { "Salario Ordinario", "Salario Retroactivo", "Reclamo Pendiente", "Incapacidad", "Incapacidad (34%)" };

            var codigoEmpleado = await _context.EusExpedienteUsuarios
                .Where(e => e.EusCodusr == user!.UsrCodigo)
                .Select(e => e.CodigoEmp)
                .FirstOrDefaultAsync();

            var actualVouchers = await _context.ReciboPagos
                .Where(v => salaryTypes.Contains(v.RpeNombreTipo) && v.RpeCodemp.ToString() == codigoEmpleado.ToString())
                .OrderByDescending(v => v.RpeFechaFin)
                .GroupBy(v => v.RpeFechaIni)
                .Select(g => g.First())
                .Take(6)
                .ToListAsync();

            var codigoExpediente = actualVouchers
                .Select(v => v.RpeCodexp)
                .FirstOrDefault();

            var vouchers = await _context.ReciboPagos
                .Where(v => salaryTypes.Contains(v.RpeNombreTipo) && v.RpeCodexp == codigoExpediente)
                .OrderByDescending(v => v.RpeFechaFin)
                .ToListAsync();

            vouchers = vouchers
                .GroupBy(v => v.RpeFechaIni)
                .Select(g => g.First())
                .Take(6)
                .ToList();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                vouchers = vouchers.Where(o =>
                    o.RpeCodpla!.Contains(searchQuery) ||
                    o.RpeFechaFinLetras!.Contains(searchQuery) ||
                    o.TplDescripcion!.Contains(searchQuery) ||
                    o.RpeNombreEmpleado!.Contains(searchQuery) ||
                    o.RpeNombreEmpresa!.Contains(searchQuery) ||
                    o.RpePuesto!.Contains(searchQuery)
                ).ToList();
            }

            var totalVouchers = vouchers.Count;

            ViewBag.SearchQuery = searchQuery;

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
                RpeCodexp = reciboPago.RpeCodexp,
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

            string htmlContent = await RenderViewToStringAsync(this, "Voucher", voucher);

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Out = null
                },
                Objects = {
                    new ObjectSettings {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8"}
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", download ? $"Recibo_{id}.pdf" : null);

        }

        public async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using var writer = new StringWriter();
            var viewResult = _viewEngine.FindView(controller.ControllerContext, viewName, false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"La vista '{viewName}' no fue encontrada.");
            }

            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.GetStringBuilder().ToString();
        }

    }
}
