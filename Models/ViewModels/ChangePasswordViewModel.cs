using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UsrUsername { get; set; } = null!;

        [Required, DataType(DataType.Password), Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; } = null!;


        [Required, DataType(DataType.Password), Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = null!;
    }
}