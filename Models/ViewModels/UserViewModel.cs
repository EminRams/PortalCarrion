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
        [RegularExpression(@"^(?=.*[A-Z]).{8,30}$", ErrorMessage = "La contraseña debe tener almenos una letra mayuscula y tener entre 8 y 30 caracteres.")]
        public string? Password { get; set; }

        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmPassword { get; set; }
    }
}