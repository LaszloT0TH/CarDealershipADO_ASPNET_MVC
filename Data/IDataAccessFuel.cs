using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessFuel
    {
        Task FuelsDelete(int id);
        Task FuelsUpdateOrInsert(FuelModel InsertedFuel);
        Task<List<FuelModel>> FuelsViewData();
        Task<List<FuelModel>> FuelsViewData(FuelModel fuelsSearch);
    }
}