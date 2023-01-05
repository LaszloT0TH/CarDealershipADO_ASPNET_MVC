﻿using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccess
    {
        Task CarAccessoriesDelete(int id);
        Task<List<CarAccessoriesModel>> CarAccessoriesListViewData();
        Task CarAccessoriesProductGroupsDelete(int id);
        Task CarAccessoriesProductGroupsUpdateOrInsert(CarAccessoriesProductGroupModel insertedCAPG);
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupsViewData();
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupsViewData(CarAccessoriesProductGroupModel sexsSearch);
        Task<List<CarAccessoriesProductGroupModel>> CarAccessoriesProductGroupViewData();
        Task CarAccessoriesUnitsDelete(int id);
        Task CarAccessoriesUnitsUpdateOrInsert(CarAccessoriesUnitModel InsertedCAU);
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitsViewData();
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitsViewData(CarAccessoriesUnitModel CAUsSearch);
        Task<List<CarAccessoriesUnitModel>> CarAccessoriesUnitViewData();
        Task CarAccessoriesUpdateOrInsert(CarAccessoriesModel insertedCarAccessories);
        Task<List<CarAccessoriesModel>> CarAccessoriesViewData();
        Task<List<CarAccessoriesModel>> CarAccessoriesViewData(CarAccessoriesModel carAccessoriesSearch);
        Task<string> CarColor(int id);
        Task<string> CarModel(int id);
        Task<string> CarsAccessoriesProductGroup(int id);
        Task<string> CarsAccessoriesProductName(int id);
        Task CarsDelete(int id);
        Task<List<CarModel>> CarsListViewData();
        Task CarsUpdateOrInsert(CarModel insertedCar);
        Task<List<CarModel>> CarsViewData();
        Task<List<CarModel>> CarsViewData(CarModel carsSearch);
        Task CountrysDelete(int id);
        Task CountrysUpdateOrInsert(CountryModel insertedCountry);
        Task<List<CountryModel>> CountrysViewData();
        Task<List<CountryModel>> CountrysViewData(CountryModel countrySearch);
        Task<List<CountryModel>> CountryViewData();
        Task<double> CustomerCountryTaxPercentageValue(int id);
        Task<string> CustomerFirstName(int id);
        Task<string> CustomerLastName(int id);
        Task CustomersDelete(int id);
        Task<List<CustomerModel>> CustomersListViewData();
        Task CustomersUpdateOrInsert(CustomerModel insertedCustomer);
        Task<List<CustomerModel>> CustomersViewData();
        Task<List<CustomerModel>> CustomersViewData(CustomerModel customerSearch);
        Task EmailUpload(string UserEmail);
        Task FuelsDelete(int id);
        Task FuelsUpdateOrInsert(FuelModel InsertedFuel);
        Task<List<FuelModel>> FuelsViewData();
        Task<List<FuelModel>> FuelsViewData(FuelModel fuelsSearch);
        Task<List<FuelModel>> FuelViewData();
        Task GearboxsDelete(int id);
        Task GearboxsUpdateOrInsert(GearboxModel InsertedGearbox);
        Task<List<GearboxModel>> GearboxsViewData();
        Task<List<GearboxModel>> GearboxsViewData(GearboxModel gearboxSearch);
        Task<List<GearboxModel>> GearboxViewData();
        Task<bool> IsManager(int UserId);
        Task<int> LoginData(string email);
        Task<List<ManagerModel>> ManagerViewData();
        Task<int> Number_of_shopping_cart_list_items_by_status(int UserId, string ShoppingCartStatusName);
        Task OrdersDelete(int id);
        Task OrderStatusDelete(int id);
        Task OrderStatusUpdateOrInsert(OrderStatusModel insertedOrderStatus);
        Task<List<OrderStatusModel>> OrderStatusViewData();
        Task<List<OrderStatusModel>> OrderStatusViewData(OrderStatusModel orderStatusSearch);
        Task OrdersUpdateOrInsert(OrderModel insertedOrder);
        Task<List<OrderModel>> OrdersViewData();
        Task<List<OrderModel>> OrdersViewData(OrderModel orderSearch);
        Task<double> SaleAmount(int id);
        Task<string> SalesPersonFirstName(int id);
        Task<string> SalesPersonLastName(int id);
        Task SalespersonsDelete(int id);
        Task<List<SalespersonModel>> SalesPersonsListViewData();
        Task SalespersonsUpdateOrInsert(SalespersonModel insertedSalesperson);
        Task<List<SalespersonModel>> SalespersonsViewData();
        Task<List<SalespersonModel>> SalespersonsViewData(SalespersonModel salespersonSearch);
        Task SexsDelete(int id);
        Task SexsUpdateOrInsert(SexModel insertedSex);
        Task<List<SexModel>> SexsViewData();
        Task<List<SexModel>> SexsViewData(SexModel sexsSearch);
        Task<List<SexModel>> SexViewData();
        Task<int> ShoppingCartCustomerIdNullCount(int UserId, string ShoppingCartStatusName);
        Task ShoppingCartDelete(int id);
        Task ShoppingCartStatusSettings(int ShoppingCartOrderId, string ShoppingCartStatusName);
        Task ShoppingCartUpdateOrInsert(ShoppingCartModel insertedShoppingCartItem);
        Task<List<ShoppingCartModel>> ShoppingCartViewData(int UserId, string ShoppingCartStatusName = null);
        Task<List<ShoppingCartModel>> ShoppingCartViewData(ShoppingCartModel shoppingCartItemSearch, int UserId);
        Task<List<GearboxModel>> SoldViewData();
        Task SpokenLanguesDelete(int id);
        Task SpokenLanguesUpdateOrInsert(SpokenLanguesModel spokenLangues);
        Task<List<SpokenLanguesModel>> SpokenLanguesViewData();
        Task<List<SpokenLanguesModel>> SpokenLanguesViewData(SpokenLanguesModel spokenLangues);
        Task<List<StockReplenishmentListModel>> StockReplenishmentListViewData();
    }
}