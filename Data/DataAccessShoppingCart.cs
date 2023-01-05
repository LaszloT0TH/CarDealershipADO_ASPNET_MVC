using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CarDealershipASPNETMVC.Data
{
    public class DataAccessShoppingCart : IDataAccessShoppingCart
    {

        static String connectionStringCarDealerShipDB = String.Empty;
        private readonly IConfiguration _config;
        string errorMessage = String.Empty;

        public DataAccessShoppingCart()
        {

        }
        public DataAccessShoppingCart(IConfiguration config)
        {
            _config = config;
            connectionStringCarDealerShipDB = _config["ConnectionStringsCarDealerShipDB"];
        }

        public async Task<List<ShoppingCartModel>> ShoppingCartViewData(int UserId, string ShoppingCartStatusName = null)
        {
            List<ShoppingCartModel> listShoppingCartAllData = new List<ShoppingCartModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("dbo.spShoppingCartSearchDynamicSQL", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@ShoppingCartStatusName", ShoppingCartStatusName);

                        command.ExecuteNonQuery();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ShoppingCartModel shoppingCart = new ShoppingCartModel();
                                shoppingCart.ShoppingCartOrderId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                                shoppingCart.UserId = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                                shoppingCart.CustomerId = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                                shoppingCart.CustomerFirstName = reader.IsDBNull(3) ? null : reader.GetString(3);
                                shoppingCart.CustomerLastName = reader.IsDBNull(4) ? null : reader.GetString(4);
                                shoppingCart.SalesPersonId = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                                shoppingCart.SalesPersonFirstName = reader.IsDBNull(6) ? null : reader.GetString(6);
                                shoppingCart.SalesPersonLastName = reader.IsDBNull(7) ? null : reader.GetString(7);
                                shoppingCart.ProductId = reader.IsDBNull(8) ? null : reader.GetInt32(8);
                                shoppingCart.ProductGroup = reader.IsDBNull(9) ? null : reader.GetString(9);
                                shoppingCart.ProductName = reader.IsDBNull(10) ? null : reader.GetString(10);
                                shoppingCart.CarModel = reader.IsDBNull(11) ? null : reader.GetString(11);
                                shoppingCart.CarColor = reader.IsDBNull(12) ? null : reader.GetString(12);
                                shoppingCart.Quantity = reader.IsDBNull(13) ? null : reader.GetInt32(13);
                                shoppingCart.CountryTaxPercentageValue = reader.IsDBNull(14) ? null : reader.GetDouble(14);
                                shoppingCart.Discount = reader.IsDBNull(15) ? null : reader.GetDouble(15);
                                shoppingCart.SaleAmount = reader.IsDBNull(16) ? null : reader.GetDouble(16);
                                shoppingCart.OrderStatusId = reader.IsDBNull(17) ? null : reader.GetInt32(17);
                                shoppingCart.OrderStatusName = reader.IsDBNull(18) ? null : reader.GetString(18);
                                shoppingCart.ShoppingCartStatusId = reader.IsDBNull(19) ? null : reader.GetInt32(19);
                                shoppingCart.ShoppingCartStatusName = reader.IsDBNull(20) ? null : reader.GetString(20);

                                listShoppingCartAllData.Add(shoppingCart);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return await Task.Run(() =>
            {
                return listShoppingCartAllData;
            });
        }

        // SEARCH
        public async Task<List<ShoppingCartModel>> ShoppingCartViewData(ShoppingCartModel shoppingCartItemSearch, int UserId)
        {
            List<ShoppingCartModel> listShoppingCartSearchData = new List<ShoppingCartModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("dbo.spShoppingCartSearchDynamicSQL", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ShoppingCartOrderId", shoppingCartItemSearch.ShoppingCartOrderId);
                        command.Parameters.AddWithValue("@UserId", shoppingCartItemSearch.UserId);
                        command.Parameters.AddWithValue("@CustomerId", shoppingCartItemSearch.CustomerId);
                        command.Parameters.AddWithValue("@CustomerFirstName", shoppingCartItemSearch.CustomerFirstName);
                        command.Parameters.AddWithValue("@CustomerLastName", shoppingCartItemSearch.CustomerLastName);
                        command.Parameters.AddWithValue("@SalesPersonId", shoppingCartItemSearch.SalesPersonId);
                        command.Parameters.AddWithValue("@SalesPersonFirstName", shoppingCartItemSearch.SalesPersonFirstName);
                        command.Parameters.AddWithValue("@SalesPersonLastName", shoppingCartItemSearch.SalesPersonLastName);
                        command.Parameters.AddWithValue("@ProductId", shoppingCartItemSearch.ProductId);
                        command.Parameters.AddWithValue("@ProductGroup", shoppingCartItemSearch.ProductGroup);
                        command.Parameters.AddWithValue("@ProductName", shoppingCartItemSearch.ProductName);
                        command.Parameters.AddWithValue("@CarModel", shoppingCartItemSearch.CarModel);
                        command.Parameters.AddWithValue("@CarColor", shoppingCartItemSearch.CarColor);
                        command.Parameters.AddWithValue("@Quantity", shoppingCartItemSearch.Quantity);
                        command.Parameters.AddWithValue("@CountryTaxPercentageValue", shoppingCartItemSearch.CountryTaxPercentageValue);
                        command.Parameters.AddWithValue("@Discount", shoppingCartItemSearch.Discount);
                        command.Parameters.AddWithValue("@SaleAmount", shoppingCartItemSearch.SaleAmount);
                        command.Parameters.AddWithValue("@OrderStatusId", shoppingCartItemSearch.OrderStatusId);
                        command.Parameters.AddWithValue("@OrderStatusName", shoppingCartItemSearch.OrderStatusName);
                        command.Parameters.AddWithValue("@ShoppingCartStatusId", shoppingCartItemSearch.ShoppingCartStatusId);
                        command.Parameters.AddWithValue("@ShoppingCartStatusName", shoppingCartItemSearch.ShoppingCartStatusName);

                        command.ExecuteNonQuery();


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ShoppingCartModel shoppingCart = new ShoppingCartModel();
                                shoppingCart.ShoppingCartOrderId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                                shoppingCart.UserId = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                                shoppingCart.CustomerId = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                                shoppingCart.CustomerFirstName = reader.IsDBNull(3) ? null : reader.GetString(3);
                                shoppingCart.CustomerLastName = reader.IsDBNull(4) ? null : reader.GetString(4);
                                shoppingCart.SalesPersonId = reader.IsDBNull(5) ? null : reader.GetInt32(5);
                                shoppingCart.SalesPersonFirstName = reader.IsDBNull(6) ? null : reader.GetString(6);
                                shoppingCart.SalesPersonLastName = reader.IsDBNull(7) ? null : reader.GetString(7);
                                shoppingCart.ProductId = reader.IsDBNull(8) ? null : reader.GetInt32(8);
                                shoppingCart.ProductGroup = reader.IsDBNull(9) ? null : reader.GetString(9);
                                shoppingCart.ProductName = reader.IsDBNull(10) ? null : reader.GetString(10);
                                shoppingCart.CarModel = reader.IsDBNull(11) ? null : reader.GetString(11);
                                shoppingCart.CarColor = reader.IsDBNull(12) ? null : reader.GetString(12);
                                shoppingCart.Quantity = reader.IsDBNull(13) ? null : reader.GetInt32(13);
                                shoppingCart.CountryTaxPercentageValue = reader.IsDBNull(14) ? null : reader.GetDouble(14);
                                shoppingCart.Discount = reader.IsDBNull(15) ? null : reader.GetDouble(15);
                                shoppingCart.SaleAmount = reader.IsDBNull(16) ? null : reader.GetDouble(16);
                                shoppingCart.OrderStatusId = reader.IsDBNull(17) ? null : reader.GetInt32(17);
                                shoppingCart.OrderStatusName = reader.IsDBNull(18) ? null : reader.GetString(18);
                                shoppingCart.ShoppingCartStatusId = reader.IsDBNull(19) ? null : reader.GetInt32(19);
                                shoppingCart.ShoppingCartStatusName = reader.IsDBNull(20) ? null : reader.GetString(20);

                                listShoppingCartSearchData.Add(shoppingCart);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return await Task.Run(() =>
            {
                return listShoppingCartSearchData;
            });
        }


        public async Task ShoppingCartUpdateOrInsert(ShoppingCartModel insertedShoppingCartItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[dbo].[spShoppingCartUpdateOrInsert]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ShoppingCartOrderId", insertedShoppingCartItem.ShoppingCartOrderId);
                        command.Parameters.AddWithValue("@UserId", insertedShoppingCartItem.UserId);
                        command.Parameters.AddWithValue("@CustomerId", insertedShoppingCartItem.CustomerId);
                        command.Parameters.AddWithValue("@CustomerFirstName", insertedShoppingCartItem.CustomerFirstName);
                        command.Parameters.AddWithValue("@CustomerLastName", insertedShoppingCartItem.CustomerLastName);
                        command.Parameters.AddWithValue("@SalesPersonId", insertedShoppingCartItem.SalesPersonId);
                        command.Parameters.AddWithValue("@SalesPersonFirstName", insertedShoppingCartItem.SalesPersonFirstName);
                        command.Parameters.AddWithValue("@SalesPersonLastName", insertedShoppingCartItem.SalesPersonLastName);
                        command.Parameters.AddWithValue("@ProductId", insertedShoppingCartItem.ProductId);
                        command.Parameters.AddWithValue("@ProductGroup", insertedShoppingCartItem.ProductGroup);
                        command.Parameters.AddWithValue("@ProductName", insertedShoppingCartItem.ProductName);
                        command.Parameters.AddWithValue("@CarModel", insertedShoppingCartItem.CarModel);
                        command.Parameters.AddWithValue("@CarColor", insertedShoppingCartItem.CarColor);
                        command.Parameters.AddWithValue("@Quantity", insertedShoppingCartItem.Quantity);
                        command.Parameters.AddWithValue("@CountryTaxPercentageValue", insertedShoppingCartItem.CountryTaxPercentageValue);
                        command.Parameters.AddWithValue("@Discount", insertedShoppingCartItem.Discount);
                        command.Parameters.AddWithValue("@SaleAmount", insertedShoppingCartItem.SaleAmount);
                        command.Parameters.AddWithValue("@OrderStatusId", insertedShoppingCartItem.OrderStatusId);
                        command.Parameters.AddWithValue("@OrderStatusName", insertedShoppingCartItem.OrderStatusName);
                        command.Parameters.AddWithValue("@ShoppingCartStatusId", insertedShoppingCartItem.ShoppingCartStatusId);
                        command.Parameters.AddWithValue("@ShoppingCartStatusName", insertedShoppingCartItem.ShoppingCartStatusName);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }

        /// <summary>
        /// for sellers only, you cannot submit an order list without a buyer
        /// Nur für Verkäufer, Sie können keine Bestellliste ohne Käufer einreichen
        /// csak eladóknak nem lehet rendelési listát leadni vásárló nélkül
        /// all items in the seller's shopping cart minus those without a customer ID. If the value is zero, then every shopping cart item has a customer
        /// alle Artikel im Warenkorb des Verkäufers abzüglich der Artikel ohne Kundennummer.Wenn der Wert null ist, dann hat jede Warenkorbposition einen Kunden
        /// az eladó összes bevásárló kosárban lévő eleme mínusz a vevő azonosító nélküliek.Ha az érték nulla, akkor minden bevásárló kosár elemhez tartozik ügyfél
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ShoppingCartStatusName"></param>
        /// <returns></returns>
        public async Task<int> ShoppingCartCustomerIdNullCount(int UserId, string ShoppingCartStatusName)
        {
            int customerIdCount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("spShoppingCartCustomerIdNullCount", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@ShoppingCartStatusName", ShoppingCartStatusName);


                        command.ExecuteNonQuery();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customerIdCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return await Task.Run(() => { return customerIdCount; });
        }

        /// <summary>
        /// Number of shopping cart list items by shopping cart status 
        /// Anzahl der Artikel der Warenkorbliste nach Warenkorbstatus
        /// Bevásárló Kosár lista tételek száma Bevásárló Kosár állapot szerint
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ShoppingCartStatusName"></param>
        /// <returns></returns>
        public async Task<int> Number_of_shopping_cart_list_items_by_status(int UserId, string ShoppingCartStatusName)
        {
            int shoppingCartListItemsByStatus = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("spNumber_of_shopping_cart_list_items_by_status", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", UserId);
                        command.Parameters.AddWithValue("@ShoppingCartStatusName", ShoppingCartStatusName);


                        command.ExecuteNonQuery();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shoppingCartListItemsByStatus = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return await Task.Run(() => { return shoppingCartListItemsByStatus; });
        }



        // Delete
        public async Task ShoppingCartDelete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStringCarDealerShipDB))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[dbo].[spShoppingCartDelete]", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@@ShoppingCartOrderId", id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }


    }
}
