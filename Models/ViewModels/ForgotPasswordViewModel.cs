using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;
    }
}