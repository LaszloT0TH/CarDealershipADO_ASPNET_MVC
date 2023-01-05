using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCarAccessories
    {
        Task CarAccessoriesDelete(int id);
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupViewData();
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitViewData();
        Task CarAccessoriesUpdateOrInsert(CarAccessoriesModel insertedCarAccessories);
        Task<List<CarAccessoriesModel>> CarAccessoriesViewData();
        Task<List<CarAccessoriesModel>> CarAccessoriesViewData(CarAccessoriesModel carAccessoriesSearch);
    }
}