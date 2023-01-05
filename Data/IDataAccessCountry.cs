using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCountry
    {
        Task CountrysDelete(int id);
        Task CountrysUpdateOrInsert(CountryModel insertedCountry);
        Task<List<CountryModel>> CountrysViewData();
        Task<List<CountryModel>> CountrysViewData(CountryModel countrySearch);
    }
}