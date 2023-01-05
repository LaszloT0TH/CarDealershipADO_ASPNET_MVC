using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessOrder
    {
        Task OrdersDelete(int id);
        Task OrdersUpdateOrInsert(OrderModel insertedOrder);
        Task<List<OrderModel>> OrdersViewData();
        Task<List<OrderModel>> OrdersViewData(OrderModel orderSearch);
        Task ShoppingCartStatusSettings(int ShoppingCartOrderId, string ShoppingCartStatusName);
    }
}