using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessSex
    {
        Task SexsDelete(int id);
        Task SexsUpdateOrInsert(SexModel insertedSex);
        Task<List<SexModel>> SexsViewData();
        Task<List<SexModel>> SexsViewData(SexModel sexsSearch);
    }
}