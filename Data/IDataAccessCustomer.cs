using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessCustomer
    {
        Task CustomersDelete(int id);
        Task CustomersInsert(CustomerInsertModel insertedCustomer);
        Task CustomersUpdateOrInsert(CustomerModel insertedCustomer);
        Task<List<CustomerModel>> CustomersViewData();
        Task<List<CustomerModel>> CustomersViewData(CustomerModel customerSearch);
    }
}