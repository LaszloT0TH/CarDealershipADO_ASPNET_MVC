using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.Models
{
    public class CustomerInsertModel
    {
        [Display(Name = "Kunde Id")]
        public int? CustomerId { get; set; }

        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Bitte eingeben den Vorname")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Bitte eingeben den Nachname")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Geschlecht")]
        [Required(ErrorMessage = "Bitte eingeben den Geschlecht")]
        public string SexName { get; set; }

        [Display(Name = "Straße")]
        [Required(ErrorMessage = "Bitte eingeben den Straßename")]
        [MaxLength(50)]
        public string Street { get; set; }

        [Display(Name = "Hausnummer")]
        [Required(ErrorMessage = "Bitte eingeben den Hausnummer")]
        [MaxLength(50)]
        public string House_Number { get; set; }

        [Display(Name = "Postleitzahl")]
        [Required(ErrorMessage = "Bitte eingeben den Postleitzahl")]
        public int PostalCode { get; set; }

        [Display(Name = "Ort")]
        [Required(ErrorMessage = "Bitte eingeben den Ort")]
        [MaxLength(50)]
        public string Location { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "Bitte eingeben den Land")]
        public string CountryName { get; set; }

        [Display(Name = "Geburtsdatum")]
        [Required(ErrorMessage = "Bitte eingeben den Geburtsdatum")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Telefonnummer \"##0 #0 ##0 ##0 ##0\"")]
        [Required(ErrorMessage = "Bitte eingeben den Telefonnummer")]
        [RegularExpression(@"^[1-9]{1}[0-9]{0,2}[ ]{1}[1-9]{1}[0-9]{0,2}[ ]{1}[1-9]{0,1}[0-9]{0,2}[ ]{0,1}[1-9]{0,1}[0-9]{0,2}[ ]{0,1}[1-9]{1}[0-9]{0,2}$")]
        public double TelNr { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bitte eingeben die EmailAdresse")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Ungültiges Email-Format")]
        [MaxLength (50)]
        public string Email { get; set; }

    }

}
