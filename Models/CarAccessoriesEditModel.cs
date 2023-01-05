namespace CarDealershipASPNETMVC.Models
{
    public class CarAccessoriesEditModel : CarAccessoriesInsertModel
    {
        public int CAId { get; set; }

        public string? ExistingPhotoPath { get; set; }
    }
}
