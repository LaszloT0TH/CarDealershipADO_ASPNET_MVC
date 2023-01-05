using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccess_HelpQuery
    {
        Task<List<CarAccessoriesModel>> CarAccessoriesListViewData();
        Task<string> CarColor(int id);
        Task<string> CarModel(int id);
        Task<string> CarsAccessoriesProductGroup(int id);
        Task<string> CarsAccessoriesProductName(int id);
        Task<List<CarModel>> CarsListViewData();
        Task<List<CountryModel>> CountryViewData();
        Task<double> CustomerCountryTaxPercentageValue(int id);
        Task<string> CustomerFirstName(int id);
        Task<string> CustomerLastName(int id);
        Task<List<CustomerModel>> CustomersListViewData();
        Task<List<ManagerModel>> ManagerViewData();
        Task<double> SaleAmount(int id);
        Task<string> SalesPersonFirstName(int id);
        Task<string> SalesPersonLastName(int id);
        Task<List<SalespersonModel>> SalesPersonsListViewData();
        Task<List<SexModel>> SexViewData();
        Task<List<StockReplenishmentListModel>> StockReplenishmentListViewData();
    }
}