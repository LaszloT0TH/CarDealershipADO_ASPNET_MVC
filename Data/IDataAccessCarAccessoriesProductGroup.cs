using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCarAccessoriesProductGroup
    {
        Task CarAccessoriesProductGroupsDelete(int id);
        Task CarAccessoriesProductGroupsUpdateOrInsert(CarAccessoriesProductGroupModel insertedCAPG);
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupsViewData();
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupsViewData(CarAccessoriesProductGroupModel sexsSearch);
    }
}