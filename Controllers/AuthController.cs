using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PortalCarrion.Models;
using PortalCarrion.Models.ViewModels;
using System.Security.Claims;
using System.Net;
using System.Net.Mail;
using NuGet.Protocol;

namespace SolicitudEmpleos2024.Controllers
{
	public class AuthController : Controller
	{
		private readonly DbA55028RecPagoCarrionContext _context;
		private readonly IConfiguration _configuration;

		public AuthController(DbA55028RecPagoCarrionContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string usr_username, string usr_password)
		{
			var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrUsername == usr_username && u.UsrActivo);

			if (user != null)
			{
				var query = "SELECT dbo.desencriptar_pass(@UsrPassword)";

				using SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString());
				connection.Open();

				using SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@UsrPassword", user.UsrPassword);

				var decryptedPassword = command.ExecuteScalar().ToString();

				if (decryptedPassword == usr_password)
				{
					if (!string.IsNullOrEmpty(user.CambioPassPrimerinicio) && user.CambioPassPrimerinicio == "no")
					{
						return RedirectToAction("Changepassword", new { usr_username = user.UsrUsername });
					}

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.UsrNombreUsuario),
						new Claim(ClaimTypes.Email, user.UsrUsername),
						new Claim(ClaimTypes.NameIdentifier, user.UsrCodigo.ToString()),
					};

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

					user.UsrUltimoAcceso = DateTime.Now;
					await _context.SaveChangesAsync();

					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
			return View();
		}

		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "Auth");
		}

		[HttpGet]
		public IActionResult ChangePassword(string usr_username)
		{
			return View(new ChangePasswordViewModel { UsrUsername = usr_username });
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrUsername == model.UsrUsername);

				Console.WriteLine(user.ToJson());

				if (user == null)
				{
					return NotFound();
				}

				var query = "SELECT dbo.ENCRIPTA_PASS(@UsrPassword)";

				using SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString());
				connection.Open();

				using SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@UsrPassword", model.NewPassword);

				var result = command.ExecuteScalar();
				byte[]? encryptedPassword = result as byte[];

				user.UsrPassword = encryptedPassword;
				user.CambioPassPrimerinicio = "si";

				await _context.SaveChangesAsync();

				TempData["SuccessMessage"] = "Contraseña cambiada con exito";


				return View("Alert", new
				{
					Title = "Contraseña Cambiada Correctamente",
					Icon = "bi-check-circle",
					Color = "text-success"
				});
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return BadRequest("El correo es requerido.");
			}

			var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrEmail == email);
			if (user == null)
			{
				return NotFound("No existe una cuenta asociada a este correo.");
			}

			var token = Guid.NewGuid().ToString();

			user.UsrTokenResetPwd = token;
			user.UsrFechaExpToken = DateTime.UtcNow.AddMinutes(10);
			await _context.SaveChangesAsync();

			var resetLink = Url.Action("ResetPassword", "Auth", new { token }, Request.Scheme);
			await SendResetEmail(email, resetLink!);

			return View("Alert",
				new
				{
					Title = "Se ha enviado un correo con las instrucciones para restablecer la contraseña.",
					Icon = "bi-info-circle",
					Color = "text-secondary"
				});
		}

		private async Task SendResetEmail(string email, string resetLink)
		{
			var smtpConfig = _configuration.GetSection("Smtp");
			var client = new SmtpClient
			{
				Host = smtpConfig["Host"],
				Port = int.Parse(smtpConfig["Port"]),
				EnableSsl = bool.Parse(smtpConfig["EnableSsl"]),
				Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"])
			};

			var message = new MailMessage
			{
				From = new MailAddress(smtpConfig["Username"]),
				Subject = "Restablecimiento de Contraseña",
				Body = $@"
					<div style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
						<div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
							<h2 style='text-align: center; color: #333333;'>Restablecimiento de Contraseña</h2>
							<hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;' />
							<p style='font-size: 16px; color: #555555; line-height: 1.5;'>
								Hola, <br /><br />
								Hemos recibido una solicitud para restablecer tu contraseña en el Portal Carrion. Si realizaste esta solicitud, haz clic en el botón a continuación. 
								Si no realizaste esta solicitud, puedes ignorar este correo de manera segura.
							</p>
							<div style='text-align: center; margin: 20px 0;'>
								<a href='{resetLink}' class='btn btn-danger  fs-4'
									style='display: inline-block; padding: 10px 20px; background-color: #e42c28; color: #ffffff; text-decoration: none; border-radius: 5px; font-size: 16px;'>
									Restablecer Contraseña
								</a>
							</div>
							<p style='font-size: 14px; color: #777777; line-height: 1.5; text-align: center;'>
								Este enlace expirará automaticamente dentro de 10 minutos. <br />
							</p>
							<hr style='border: 0; border-top: 1px solid #eeeeee; margin: 20px 0;' />
							<p style='font-size: 12px; color: #aaaaaa; text-align: center;'>
								Este es un correo automático. Por favor, no respondas a este mensaje.
							</p>
						</div>
					</div>",
				IsBodyHtml = true
			};
			message.To.Add(email);

			await client.SendMailAsync(message);
		}

		[HttpGet]
		public IActionResult ResetPassword(string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				return View("Alert",
					new
					{
						Title = "Token No Válido",
						Icon = "bi-x-circle",
						Color = "text-danger"
					});
			}

			return View(new ResetPasswordViewModel { Token = token });
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			var user = await _context.UsrUsers.SingleOrDefaultAsync(u => u.UsrTokenResetPwd == model.Token);
			if (user == null || user.UsrFechaExpToken < DateTime.UtcNow)
			{
				return View("Alert",
					new
					{
						Title = "El token es inválido o ha expirado.",
						Icon = "bi-x-circle",
						Color = "text-danger"
					});
			}

			// encrypt password
			var query = "SELECT dbo.ENCRIPTA_PASS(@UsrPassword)";

			using SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString());
			connection.Open();

			using SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@UsrPassword", model.NewPassword);

			var result = command.ExecuteScalar();
			byte[]? encryptedPassword = result as byte[];

			// TempData["SuccessMessage"] = "Contraseña cambiada con exito";
			user.UsrPassword = encryptedPassword;
			user.UsrTokenResetPwd = null;
			user.UsrFechaExpToken = null;

			await _context.SaveChangesAsync();

			return View("Alert",
				new
				{
					Title = "Su contraseña ha sido reestablecida correctamente.",
					Icon = "bi-check-circle",
					Color = "text-success"
				});
		}
	}
}
