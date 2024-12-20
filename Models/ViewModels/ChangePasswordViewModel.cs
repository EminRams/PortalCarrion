using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UsrUsername { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña no puede estar vacía"), DataType(DataType.Password), Display(Name = "Nueva Contraseña")]
        [RegularExpression(@"^(?=.*[A-Z]).{8,30}$", ErrorMessage = "La contraseña debe tener almenos una letra mayuscula y tener entre 8 y 30 caracteres.")]
        public string NewPassword { get; set; } = null!;


        [Required(ErrorMessage = "La contraseña no puede estar vacía"), DataType(DataType.Password), Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        [RegularExpression(@"^(?=.*[A-Z]).{8,30}$", ErrorMessage = "La contraseña debe tener almenos una letra mayuscula y tener entre 8 y 30 caracteres.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}