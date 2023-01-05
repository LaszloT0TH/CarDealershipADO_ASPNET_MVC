using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarDealershipASPNETMVC.Models
{
    public class ShoppingCartModel
    {

        [HiddenInput(DisplayValue = false)]
        public int? ShoppingCartOrderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? CustomerId { get; set; }

        [Display(Name = "Kunde Vorname")]
        public string? CustomerFirstName { get; set; }

        [Display(Name = "Kunde Nachname")]
        public string? CustomerLastName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? SalesPersonId { get; set; }

        [Display(Name = "Verkäufer Vorname")]
        public string? SalesPersonFirstName { get; set; }

        [Display(Name = "Verkäufer Nachname")]
        public string? SalesPersonLastName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Produkt Id")]
        public int? ProductId { get; set; }

        public int? ProductIdVisible
        {
            get
            {
                return ProductId;
            }
        }


        [Display(Name = "Produktgruppe")]
        public string? ProductGroup { get; set; }

        [Display(Name = "Produktname")]
        public string? ProductName { get; set; }

        [Display(Name = "Auto Model")]
        public string? CarModel { get; set; }

        [Display(Name = "Autofarbe")]
        public string CarColor { get; set; }

        [Display(Name = "Menge")]
        public int? Quantity { get; set; }

        [Display(Name = "Steuerprozentsatz des Landes")]
        public double? CountryTaxPercentageValue { get; set; }

        [Display(Name = "Rabatt")]
        public double? Discount { get; set; }

        [Display(Name = "Netto Verkaufsbetrag")]
        public double? SaleAmount { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? OrderStatusId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? OrderStatusName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ShoppingCartStatusId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? ShoppingCartStatusName { get; set; }

    }

}
