using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessGearbox
    {
        Task GearboxsDelete(int id);
        Task GearboxsUpdateOrInsert(GearboxModel InsertedGearbox);
        Task<List<GearboxModel>> GearboxsViewData();
        Task<List<GearboxModel>> GearboxsViewData(GearboxModel gearboxSearch);
    }
}