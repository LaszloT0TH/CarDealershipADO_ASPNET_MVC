using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessSpokenLangues
    {
        Task SpokenLanguesDelete(int id);
        Task SpokenLanguesUpdateOrInsert(SpokenLanguesModel spokenLangues);
        Task<List<SpokenLanguesModel>> SpokenLanguesViewData();
        Task<List<SpokenLanguesModel>> SpokenLanguesViewData(SpokenLanguesModel spokenLangues);
    }
}