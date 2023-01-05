using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bitte eingeben die EmailAdresse")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "Ungültiges Email-Format")]
        [MaxLength(50)]
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}
