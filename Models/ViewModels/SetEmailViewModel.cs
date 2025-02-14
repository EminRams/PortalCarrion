using System.ComponentModel.DataAnnotations;

namespace PortalCarrion.Models.ViewModels
{
    public class SetEmailViewModel
    {
        public string UsrUsername { get; set; } = null!;

        [Required(ErrorMessage = "Ingrese un correo electrónico"), DataType(DataType.EmailAddress), Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;
    }
}