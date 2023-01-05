using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCarAccessoriesUnit
    {
        Task CarAccessoriesUnitsDelete(int id);
        Task CarAccessoriesUnitsUpdateOrInsert(CarAccessoriesUnitModel InsertedCAU);
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitsViewData();
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitsViewData(CarAccessoriesUnitModel CAUsSearch);
    }
}