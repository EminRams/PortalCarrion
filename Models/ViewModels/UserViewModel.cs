using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class UserViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Identidad")]
        public string Username { get; set; } = null!;

        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Display(Name = "Correo Electrónico")]
        public string? Email { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Nueva Contraseña")]
        public string? Password { get; set; }

        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmPassword { get; set; }
    }
}