using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipASPNETMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDataAccessShoppingCart dataAccessShoppingCart;
        private readonly IDataAccess_HelpQuery dataAccess_HelpQuery;
        private readonly IDataAccessOrder dataAccessOrder;
        private readonly IDataAccessOrderStatus dataAccessOrderStatus;

        public ShoppingCartController(IDataAccessShoppingCart dataAccessShoppingCart,
                                      IDataAccess_HelpQuery dataAccess_HelpQuery,
                                      IDataAccessOrder dataAccessOrder,
                                      IDataAccessOrderStatus dataAccessOrderStatus)
        {
            this.dataAccessShoppingCart = dataAccessShoppingCart;
            this.dataAccess_HelpQuery = dataAccess_HelpQuery;
            this.dataAccessOrder = dataAccessOrder;
            this.dataAccessOrderStatus = dataAccessOrderStatus;
        }


        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Shopping Cart";

            if (GlobalData.First_Entry)
            {
                GlobalData.First_Entry = false;
                GlobalData.ShoppingCartStatus = "Im Einkaufswagen";
            }
            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId, GlobalData.ShoppingCartStatus);

            // for sellers only, you cannot submit an order list without a buyer
            // Nur für Verkäufer, Sie können keine Bestellliste ohne Käufer einreichen
            // csak eladóknak nem lehet rendelési listát leadni vásárló nélkül
            // all items in the seller's shopping cart minus those without a customer ID. If the value is zero, then every shopping cart item has a customer
            // alle Artikel im Warenkorb des Verkäufers abzüglich der Artikel ohne Kundennummer.Wenn der Wert null ist, dann hat jede Warenkorbposition einen Kunden
            // az eladó összes bevásárló kosárban lévő eleme mínusz a vevő azonosító nélküliek.Ha az érték nulla, akkor minden bevásárló kosár elemhez tartozik ügyfél
            ViewBag.CustomerIdCountNullCount = await dataAccessShoppingCart.ShoppingCartCustomerIdNullCount(GlobalData.UserId, GlobalData.ShoppingCartStatus);

            // if the list contains an item, the button is displayed
            // Wenn die Liste ein Element enthält, wird die Schaltfläche angezeigt
            // ha a lista tartalmaz elemet, akkor megjelenik a gomb
            ViewBag.Im_Einkaufswagen = await dataAccessShoppingCart.Number_of_shopping_cart_list_items_by_status(GlobalData.UserId, "Im Einkaufswagen");
            ViewBag.Bestellt = await dataAccessShoppingCart.Number_of_shopping_cart_list_items_by_status(GlobalData.UserId, "Bestellt");
            ViewBag.Für_später_gespeichert = await dataAccessShoppingCart.Number_of_shopping_cart_list_items_by_status(GlobalData.UserId, "Für später gespeichert");
            ViewBag.Unterwegs = await dataAccessShoppingCart.Number_of_shopping_cart_list_items_by_status(GlobalData.UserId, "Unterwegs");
            ViewBag.Zugestellt = await dataAccessShoppingCart.Number_of_shopping_cart_list_items_by_status(GlobalData.UserId, "Zugestellt");

            // The buy button is not visible until the list is empty
            // Der Kaufen-Button ist erst sichtbar, wenn die Liste leer ist
            // Amíg a lista üres addig nem látszódik az vásárlás gomb
            ViewBag.NewOrdersListCount = listShoppingCart.Count;


            return await Task.Run(() => View("Index", listShoppingCart));
        }

        [HttpGet]
        public async Task<IActionResult> In_the_shopping_cart()
        {
            GlobalData.ShoppingCartStatus = "Im Einkaufswagen";

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }
        
        [HttpGet]
        public async Task<IActionResult> Ordered()
        {
            GlobalData.ShoppingCartStatus = "Bestellt";


            return await Task.Run(() => { return RedirectToAction("Index"); });
        }

        [HttpGet]
        public async Task<IActionResult> Saved_for_later(int id)
        {
            GlobalData.ShoppingCartStatus = "Für später gespeichert";

            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            foreach (ShoppingCartModel shoppingCartItem in listShoppingCart)
            {
                if (shoppingCartItem.ShoppingCartStatusName == "Im Einkaufswagen" && shoppingCartItem.ShoppingCartOrderId == id)
                {
                    shoppingCartItem.ShoppingCartStatusName = "Für später gespeichert";

                    // add ordered status
                    // Bestellstatus hinzufügen
                    // megrendelt státusz hozzáadása
                    await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(shoppingCartItem);
                }

            }

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }
        [HttpGet]
        public async Task<IActionResult> Add_to_Saved_for_later_list(int id)
        {
            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            foreach (ShoppingCartModel shoppingCartItem in listShoppingCart)
            {
                if (shoppingCartItem.ShoppingCartStatusName == "Im Einkaufswagen" && shoppingCartItem.ShoppingCartOrderId == id)
                {
                    shoppingCartItem.ShoppingCartStatusName = "Für später gespeichert";

                    // add ordered status
                    // Bestellstatus hinzufügen
                    // megrendelt státusz hozzáadása
                    await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(shoppingCartItem);
                }

            }

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }
        
        [HttpGet]
        public async Task<IActionResult> Add_to_shopping_cart_list(int id)
        {
            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            foreach (ShoppingCartModel shoppingCartItem in listShoppingCart)
            {
                if (shoppingCartItem.ShoppingCartStatusName == "Für später gespeichert" && shoppingCartItem.ShoppingCartOrderId == id)
                {
                    shoppingCartItem.ShoppingCartStatusName = "Im Einkaufswagen";

                    // add ordered status
                    // Bestellstatus hinzufügen
                    // megrendelt státusz hozzáadása
                    await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(shoppingCartItem);
                }

            }

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }

        [HttpGet]
        public async Task<IActionResult> In_transit()
        {
            GlobalData.ShoppingCartStatus = "Unterwegs";
            
            return await Task.Run(() => { return RedirectToAction("Index"); });
        }
        
        [HttpGet]
        public async Task<IActionResult> Shipped()
        {
            GlobalData.ShoppingCartStatus = "Zugestellt";

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }




        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Shopping Cart Edit";

            // Buyer can edit the quantity
            // Seller can edit quantity and add customer
            // Manager can edit the quantity, add buyer and seller

            // Der Käufer kann die Menge bearbeiten
            // Der Verkäufer kann die Menge bearbeiten und einen Kunden hinzufügen
            // Der Manager kann die Menge bearbeiten, Käufer und Verkäufer hinzufügen

            // Vásárló a mennyiséget tudja szerkeszteni
            // Eladó a mennyiséget tudja szerkeszteni és vásárlót tud hozzáadni
            // Manager a mennyiséget tudja szerkeszteni, vásárlót és eladót tud hozzáadni

            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            ViewBag.CustomersList = await dataAccess_HelpQuery.CustomersListViewData();
            ViewBag.SalesPersonsList = await dataAccess_HelpQuery.SalesPersonsListViewData();
            ViewBag.CarAccessoriesList = await dataAccess_HelpQuery.CarAccessoriesListViewData();
            ViewBag.OrderStatusList = await dataAccessOrderStatus.OrderStatusViewData();

            ShoppingCartModel findShoppingCartItem = listShoppingCart.Single(shoppingCart => shoppingCart.ShoppingCartOrderId == id);

            ViewBag.ProductName = findShoppingCartItem.ProductName;

            return await Task.Run(() => { return View(findShoppingCartItem); });
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(ShoppingCartModel modelshoppingCart)
        {
            if (GlobalData.UserAccess != "Customer")
            {
                List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

                ShoppingCartModel findShoppingCartItem = listShoppingCart.Single(shoppingCart => shoppingCart.ShoppingCartOrderId == modelshoppingCart.ShoppingCartOrderId);

                await TryUpdateModelAsync(findShoppingCartItem);

                await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(findShoppingCartItem);

            }
            else
            {
                List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

                ShoppingCartModel findShoppingCartItem = listShoppingCart.Single(shoppingCart => shoppingCart.ShoppingCartOrderId == modelshoppingCart.ShoppingCartOrderId);

                await TryUpdateModelAsync(findShoppingCartItem);

                // If it's a customer, add the automatic order taker
                // Wenn es sich um einen Kunden handelt, fügen Sie den automatischen Order Taker hinzu
                // Ha vásárló akkor az automata rendelésfelvevő hozzáadása
                findShoppingCartItem.SalesPersonId = 0;

                await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(findShoppingCartItem);

            }
            return await Task.Run(() => { return RedirectToAction("Index"); });

        }


        // Delete rows
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessShoppingCart.ShoppingCartDelete(id);

            return await Task.Run(() => { return RedirectToAction("Index"); });
        }


        // Vásárlók hozzásadása a lista minden eleméhez
        [HttpGet]
        [ActionName("AddCustoner")]
        public async Task<IActionResult> AddCustoner_Get()
        {
            ViewData["Title"] = "Shopping Cart Add Custoner";

            // add the selected customer ID to all orders
            // die ausgewählte Kunden-ID zu allen Bestellungen hinzufügen
            // hozzáadja az összes rendelésekhez a választott ögyfélazonosítót
            ViewBag.CustomersList = await dataAccess_HelpQuery.CustomersListViewData();

            return await Task.Run(() => { return View(); });
        }

        /// <summary>
        /// in any case, add the customers we are currently working with to the list
        /// Fügen Sie in jedem Fall die Kunden, mit denen wir derzeit zusammenarbeiten, zur Liste hinzu
        /// minden esetben ahhoz a listához adja hozzás a vásárlókat amelyikkel éppen dolgozunk
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddCustoner")]
        public async Task<IActionResult> AddCustoner_Post(CustomerModel customer)
        {
            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            foreach (ShoppingCartModel shoppingCartItem in listShoppingCart)
            {
                if (shoppingCartItem.ShoppingCartStatusName == GlobalData.ShoppingCartStatus)
                {
                    shoppingCartItem.CustomerId = customer.CustomerId;

                    // adding a customer to all orders
                    // Hinzufügen eines Kunden zu allen Bestellungen
                    // egy vásárló hozzáadása az összes rendeléshez
                    await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(shoppingCartItem);
                }
            }

            return RedirectToAction("Index");
        }




        //sending draft order
        //rendelési tervezet elküldése
        [HttpGet]
        [ActionName("AddOrder")]
        public async Task<IActionResult> AddOrder()
        {
            List<ShoppingCartModel> listShoppingCart = await dataAccessShoppingCart.ShoppingCartViewData(GlobalData.UserId);

            foreach (ShoppingCartModel shoppingCartItem in listShoppingCart)
            {
                if (shoppingCartItem.ShoppingCartStatusName == GlobalData.ShoppingCartStatus)
                {
                    OrderModel newOrder = new OrderModel();

                    newOrder.CustomerId = shoppingCartItem.CustomerId;
                    newOrder.SalesPersonId = shoppingCartItem.SalesPersonId;
                    newOrder.ProductId = shoppingCartItem.ProductId;
                    newOrder.Quantity = shoppingCartItem.Quantity;
                    newOrder.OrderStatusId = shoppingCartItem.OrderStatusId;
                    newOrder.Discount = shoppingCartItem.Discount;
                    newOrder.ShoppingCartOrderId = shoppingCartItem.ShoppingCartOrderId;

                    shoppingCartItem.ShoppingCartStatusName = "Bestellt";

                    // insert into the orders table
                    // in die Auftragstabelle einfügen
                    // beszúrás a rendelések táblázatba
                    await dataAccessOrder.OrdersUpdateOrInsert(newOrder);

                    // add ordered status
                    // Bestellstatus hinzufügen
                    // megrendelt státusz hozzáadása
                    await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(shoppingCartItem);
                }

            }

            GlobalData.ShoppingCartStatus = "Bestellt";

            return RedirectToAction("Index");
        }

    }
}
