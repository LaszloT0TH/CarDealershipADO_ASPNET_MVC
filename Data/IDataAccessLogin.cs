namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessLogin
    {
        Task EmailUpload(string UserEmail);
        Task<bool> IsManager(int UserId);
        Task<int> LoginData(string email);
    }
}