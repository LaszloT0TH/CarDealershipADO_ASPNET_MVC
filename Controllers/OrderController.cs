using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipASPNETMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDataAccessOrder dataAccessOrder;
        private readonly IDataAccess_HelpQuery dataAccess_HelpQuery;
        private readonly IDataAccessCarAccessories dataAccessCarAccessories;
        private readonly IDataAccessOrderStatus dataAccessOrderStatus;

        public OrderController(IDataAccessOrder dataAccessOrder,
                                IDataAccessCarAccessories dataAccessCarAccessories,
                                IDataAccessOrderStatus dataAccessOrderStatus,
                                IDataAccess_HelpQuery dataAccess_HelpQuery)
        {
            this.dataAccessOrder = dataAccessOrder;
            this.dataAccessCarAccessories = dataAccessCarAccessories;
            this.dataAccessOrderStatus = dataAccessOrderStatus;
            this.dataAccess_HelpQuery = dataAccess_HelpQuery;
        }

        /// <summary>
        /// Order
        /// </summary>
        /// <returns></returns>
        // View Car Accessories All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Orders";

            List<OrderModel> ListOrders = await dataAccessOrder.OrdersViewData();

            List<StockReplenishmentListModel> StockReplenishmentList = await dataAccess_HelpQuery.StockReplenishmentListViewData();
            ViewBag.StockReplenishmentListCount = StockReplenishmentList.Count;

            return await Task.Run(() => View("Index", ListOrders));
        }

        // Order Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Orders Search";

            ViewBag.CustomersList = await dataAccess_HelpQuery.CustomersListViewData();
            ViewBag.SalesPersonsList = await dataAccess_HelpQuery.SalesPersonsListViewData();
            ViewBag.CarAccessoriesList = await dataAccess_HelpQuery.CarAccessoriesListViewData();
            ViewBag.CarAccessoriesProductGroupList = await dataAccessCarAccessories.CarAccessoriesProductGroupViewData();
            ViewBag.OrderStatusList = await dataAccessOrderStatus.OrderStatusViewData();
            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            OrderModel searchedOrder = new OrderModel();

            await TryUpdateModelAsync(searchedOrder);

            List<OrderModel> listOrdersSearchResult = await dataAccessOrder.OrdersViewData(searchedOrder);

            return await Task.Run(() => View("Index", listOrdersSearchResult));

        }


        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Orders Edit";

            // You either sell the car or you don't. It is not possible to edit a car or change it to a car part,
            // if you didn't want a car originally, you have to delete it
            // the online shop is only available for spare parts, the car's sold status is automatically reset to false in the database when deleted
            // Entweder du verkaufst das Auto oder nicht. Es ist nicht möglich, ein Auto zu bearbeiten oder in ein Autoteil umzuwandeln,
            // wenn Sie ein Auto ursprünglich nicht wollten, müssen Sie es löschen
            // Der Online-Shop ist nur für Ersatzteile verfügbar, der Verkauft-Status des Autos wird beim Löschen automatisch in der Datenbank auf false zurückgesetzt
            // Az autót vagy eladja vagy nem. Nincs lehetőség autót szerkeszteni autóalkatrészre változtatni, ha nem autót akart volna eredetileg akkor törölni kell
            // az online shop csak az alkatrészekhez van törlésnél az adatbankban automatikusan visszaáll az autó eladott státusza false értékre
            ViewBag.CustomersList = await dataAccess_HelpQuery.CustomersListViewData();
            ViewBag.SalesPersonsList = await dataAccess_HelpQuery.SalesPersonsListViewData();
            ViewBag.CarAccessoriesList = await dataAccess_HelpQuery.CarAccessoriesListViewData();
            ViewBag.OrderStatusList = await dataAccessOrderStatus.OrderStatusViewData();

            List<OrderModel> listOrders = await dataAccessOrder.OrdersViewData();

            OrderModel findOrder = listOrders.Single(order => order.OrderId == id);

            return View(findOrder);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(OrderModel modelOrder)
        {
            List<OrderModel> listOrders = await dataAccessOrder.OrdersViewData();

            OrderModel findUpdatedOrder = listOrders.Single(order => order.OrderId == modelOrder.OrderId);

            await TryUpdateModelAsync(findUpdatedOrder);


            // after processing, you will receive the SalesPerson's ID
            // Nach der Bearbeitung erhalten Sie die Verkäufer-ID
            // feldolgozás után megkapja az eladó azonosítóját
            if (findUpdatedOrder.SalesPersonId == 0)
            {
                findUpdatedOrder.SalesPersonId = GlobalData.UserId;
            }

            // if the seller accepted the order for further processing
            // wenn der Verkäufer die Bestellung zur weiteren Bearbeitung angenommen hat
            // ha az eladó a rendelést további feldolgozásra átvette
            if (GlobalData.UserAccess == "Salesperson")
            {
                findUpdatedOrder.OrderStatusId = 2;
            }

            // if the amount received is equal to or greater than the amount to be paid,
            // the order status is set to completed and the sale time is time stamped
            // ha a beérkezett összeg egyenlő vagy nagyobb mint fizetendő összeg, akkor a rendelési
            // a rendelési státuszt befejezettre állítjuk az eladási időt pedig időbélyegzővel látjuk el
            if (findUpdatedOrder.SaleAmountPaid >= findUpdatedOrder.SaleAmount)
            {
                // possible connection point for financial and logistics units
                // möglicher Verbindungspunkt für Finanz- und Logistikeinheiten
                // lehetséges kapcsolódás pont pénzügyi  és logisztikai egységeknek
                findUpdatedOrder.OrderStatusId = 4;
                findUpdatedOrder.SaleTime = DateTime.Now;

                // the shopping cart status = on the way, if the money has been received and the package is ready and handed over to the courier service
                // der Warenkorbstatus = unterwegs, wenn das Geld eingegangen ist und das Paket fertig ist und dem Kurierdienst übergeben wird
                // a bevásárló kosár státusz = úton, ha a pénz beérkezett és a csomag kész és fel lett adva a futárszolgálatnak
                await dataAccessOrder.ShoppingCartStatusSettings(Convert.ToInt32(findUpdatedOrder.ShoppingCartOrderId), "Unterwegs");
            }

            await dataAccessOrder.OrdersUpdateOrInsert(findUpdatedOrder);

            return RedirectToAction("Index");
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessOrder.OrdersDelete(id);

            return RedirectToAction("Index");
        }


        // StockReplenishmentList
        [HttpGet]
        public async Task<IActionResult> StockReplenishmentList()
        {
            ViewData["Title"] = "Stock Replenishment List";
            
            List<StockReplenishmentListModel> StockReplenishmentList = await dataAccess_HelpQuery.StockReplenishmentListViewData();

            return await Task.Run(() => View("StockReplenishmentList", StockReplenishmentList));
        }
    }
}
