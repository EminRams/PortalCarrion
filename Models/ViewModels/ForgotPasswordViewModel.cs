using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Ingrese un correo electrónico"), DataType(DataType.EmailAddress), Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;
    }
}