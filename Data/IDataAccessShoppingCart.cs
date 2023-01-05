using CarDealershipASPNETMVC.Models;

namespace CarDealershipASPNETMVC.Data
{
    public interface IDataAccessShoppingCart
    {
        Task<int> Number_of_shopping_cart_list_items_by_status(int UserId, string ShoppingCartStatusName);
        Task<int> ShoppingCartCustomerIdNullCount(int UserId, string ShoppingCartStatusName);
        Task ShoppingCartDelete(int id);
        Task ShoppingCartUpdateOrInsert(ShoppingCartModel insertedShoppingCartItem);
        Task<List<ShoppingCartModel>> ShoppingCartViewData(int UserId, string ShoppingCartStatusName = null);
        Task<List<ShoppingCartModel>> ShoppingCartViewData(ShoppingCartModel shoppingCartItemSearch, int UserId);
    }
}