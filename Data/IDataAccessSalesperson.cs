using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessSalesperson
    {
        Task SalespersonsDelete(int id);
        Task SalespersonsUpdateOrInsert(SalespersonModel insertedSalesperson);
        Task<List<SalespersonModel>> SalespersonsViewData();
        Task<List<SalespersonModel>> SalespersonsViewData(SalespersonModel salespersonSearch);
    }
}