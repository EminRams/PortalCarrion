using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PortalCarrion.Models;
using PortalCarrion.Models.ViewModels;

namespace PortalCarrion.Controllers
{
    [Authorize]
    public class UserConfigController : Controller
    {
        private readonly DbA55028RecPagoCarrionContext _context;

        public UserConfigController(DbA55028RecPagoCarrionContext context)
        {
            _context = context;
        }

        // GET: UserConfig/Edit/5
        public async Task<IActionResult> Edit()
        {
            var userClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrCodigo.ToString() == userClaim);

            var model = new UserViewModel
            {
                Codigo = user.UsrCodigo,
                Username = user.UsrUsername,
                Name = user.UsrNombreUsuario,
                Email = user.UsrEmail,
            };

            return View(model);
        }

        // POST: UserConfig/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var existingUser = await _context.UsrUsers.FirstOrDefaultAsync(u => u.UsrCodigo == model.Codigo);

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

            existingUser.UsrEmail = model.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
