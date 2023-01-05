using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessOrderStatus
    {
        Task OrderStatusDelete(int id);
        Task OrderStatusUpdateOrInsert(OrderStatusModel insertedOrderStatus);
        Task<List<OrderStatusModel>> OrderStatusViewData();
        Task<List<OrderStatusModel>> OrderStatusViewData(OrderStatusModel orderStatusSearch);
    }
}