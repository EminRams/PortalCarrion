using System.Numerics;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using PortalCarrion.Models;
using PortalCarrion.Models.ViewModels;

namespace SolicitudEmpleos2024.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly DbA55028RecPagoCarrionContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(DbA55028RecPagoCarrionContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string password)
        {
            var masterPassword = _configuration["Admin:MasterPassword"];

            if (password == masterPassword)
            {
                HttpContext.Session.SetString("IsAdminAuthenticated", "true");
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Contraseña Incorrecta");
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("IsAdminAuthenticated");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery, int pageSize = 10, int page = 1)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var usrUsersQuery = _context.UsrUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                usrUsersQuery = usrUsersQuery.Where(a =>
                    a.UsrCodigo.ToString().Contains(searchQuery) ||
                    a.UsrUsername.Contains(searchQuery) ||
                    a.UsrNombreUsuario.Contains(searchQuery) ||
                    a.UsrActivo.ToString().Contains(searchQuery) ||
                    a.UsrEmail.Contains(searchQuery) ||
                    a.UsrUltimoAcceso.ToString().Contains(searchQuery) ||
                    a.UsrModoAutenticacion.Contains(searchQuery));
            }

            var totalUsers = await _context.UsrUsers.CountAsync();

            var users = await usrUsersQuery
                .OrderBy(u => u.UsrCodigo == u.UsrCodigo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsQueryable()
                .ToListAsync();

            ViewBag.SearchQuery = searchQuery;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            return View(users);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var user = await _context.UsrUsers
                .FirstOrDefaultAsync(m => m.UsrCodigo == id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Codigo = user.UsrCodigo,
                Username = user.UsrUsername,
                Name = user.UsrNombreUsuario,
                Active = user.UsrActivo,
                Email = user.UsrEmail,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var user = await _context.UsrUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Codigo = user.UsrCodigo,
                Username = user.UsrUsername,
                Name = user.UsrNombreUsuario,
                Active = user.UsrActivo,
                Email = user.UsrEmail,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var existingUser = await _context.UsrUsers.FirstOrDefaultAsync(u => u.UsrCodigo == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                var query = "SELECT dbo.ENCRIPTA_PASS(@UsrPassword)";
                using SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString());
                connection.Open();

                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UsrPassword", model.Password);

                var result = command.ExecuteScalar();
                byte[]? encryptedPassword = result as byte[];
                existingUser.UsrPassword = encryptedPassword;
            }

            existingUser.UsrUsername = model.Username;
            existingUser.UsrNombreUsuario = model.Name;
            existingUser.UsrActivo = model.Active;
            existingUser.UsrEmail = model.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Disable(int? id)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var user = await _context.UsrUsers
                .FirstOrDefaultAsync(m => m.UsrCodigo == id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Codigo = user.UsrCodigo,
                Username = user.UsrUsername,
                Name = user.UsrNombreUsuario,
                Active = user.UsrActivo,
                Email = user.UsrEmail,
            };

            return View(model);
        }

        [HttpPost, ActionName("Disable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id)
        {
            if (HttpContext.Session.GetString("IsAdminAuthenticated") != "true")
            {
                return RedirectToAction("AdminLogin");
            }

            var user = await _context.UsrUsers.FindAsync(id);

            if (user != null)
            {
                user.UsrActivo = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}