using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCars
    {
        Task CarsDelete(int id);
        Task CarsUpdateOrInsert(CarModel insertedCar);
        Task<List<CarModel>> CarsViewData();
        Task<List<CarModel>> CarsViewData(CarModel carsSearch);
        Task<List<FuelModel>> FuelViewData();
        Task<List<GearboxModel>> GearboxViewData();
        Task<List<GearboxModel>> SoldViewData();
    }
}