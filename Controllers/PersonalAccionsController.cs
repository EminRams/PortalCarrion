using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortalCarrion.Models;

namespace PortalCarrion.Controllers
{
    public class PersonalAccionsController : Controller
    {
        private readonly DbA55028RecPagoCarrionContext _context;

        public PersonalAccionsController(DbA55028RecPagoCarrionContext context)
        {
            _context = context;
        }

        // GET: PersonalAccions
        public async Task<IActionResult> Index()
        {
            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrCodigo.ToString() == userClaim);

            var employee = await _context.Empleados
                .Where(e => e.EmpCodigoUsuario == user!.UsrCodigo)
                .FirstOrDefaultAsync();

            var personalAccions = _context.AccionesPersonals
                // .Where(a => a.AcpCodexpEmpleado == employee.EmpCodigoAlternativo) // this wont work until we have the employee data
                .ToList();

            return View(personalAccions);
        }

        // GET: PersonalAccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accionesPersonal = await _context.AccionesPersonals
                .FirstOrDefaultAsync(m => m.AcpId == id);
            if (accionesPersonal == null)
            {
                return NotFound();
            }

            return View(accionesPersonal);
        }

        // GET: PersonalAccions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalAccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccionesPersonal accionesPersonal)
        {
            if (ModelState.IsValid)
            {
                accionesPersonal.AcpEstado = "Pendiente";
                _context.Add(accionesPersonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accionesPersonal);
        }

        // GET: PersonalAccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accionesPersonal = await _context.AccionesPersonals.FindAsync(id);

            if (accionesPersonal == null)
            {
                return NotFound();
            }
            return View(accionesPersonal);
        }

        // POST: PersonalAccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcpId,AcpFechaCreacion,AcpCodexpEmpleado,AcpNombreEmpleado,AcpCompaniaEmpleado,AcpTiendaEmpleado,AcpDepartamentoEmpleado,AcpPuestoEmpleado,AcpFechaIngresoEmpleado,AcpMotivo,AcpNuevoSalario,AcpNuevaTienda,AcpNuevoDepartamento,AcpNuevoPuesto,AcpNuevaCondicion,AcpEstado,AcpFechaRigePartir,AcpFechaRigeHasta")] AccionesPersonal accionesPersonal)
        {
            if (id != accionesPersonal.AcpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accionesPersonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccionesPersonalExists(accionesPersonal.AcpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accionesPersonal);
        }

        // GET: PersonalAccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accionesPersonal = await _context.AccionesPersonals
                .FirstOrDefaultAsync(m => m.AcpId == id);
            if (accionesPersonal == null)
            {
                return NotFound();
            }

            return View(accionesPersonal);
        }

        // POST: PersonalAccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accionesPersonal = await _context.AccionesPersonals.FindAsync(id);
            if (accionesPersonal != null)
            {
                _context.AccionesPersonals.Remove(accionesPersonal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccionesPersonalExists(int id)
        {
            return _context.AccionesPersonals.Any(e => e.AcpId == id);
        }
    }
}
