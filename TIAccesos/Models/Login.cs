using System.ComponentModel.DataAnnotations;

namespace TIAccesos.Models
{
    public class Login
    {
        [Display(Name = "Correo:")]
        public string Email { get; set; }

        [Display(Name = "Clave de acceso:")]

        [DataType
        (DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}