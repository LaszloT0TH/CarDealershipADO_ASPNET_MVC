using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsOrderStatusController : Controller
    {
        private readonly IDataAccessOrderStatus dataAccessOrderStatus;

        public SettingsOrderStatusController(IDataAccessOrderStatus dataAccessOrderStatus)
        {
            this.dataAccessOrderStatus = dataAccessOrderStatus;
        }


        // View All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Order Status Settings";

            List<OrderStatusModel> ListOrderStatus = await dataAccessOrderStatus.OrderStatusViewData();

            return await Task.Run(() => View("Index", ListOrderStatus));
        }

        // Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Order Status Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            OrderStatusModel searchedOrderStatus = new OrderStatusModel();

            await TryUpdateModelAsync(searchedOrderStatus);

            List<OrderStatusModel> listOrderStatusSearchResult = await dataAccessOrderStatus.OrderStatusViewData(searchedOrderStatus);

            return await Task.Run(() => View("Index", listOrderStatusSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Order Status Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                OrderStatusModel insertedOrderStatus = new OrderStatusModel();

                await TryUpdateModelAsync(insertedOrderStatus);

                await dataAccessOrderStatus.OrderStatusUpdateOrInsert(insertedOrderStatus);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Order Status Edit";

            List<OrderStatusModel> listOrderStatus = await dataAccessOrderStatus.OrderStatusViewData();

            OrderStatusModel findOrderStatus = listOrderStatus.Single(OS => OS.OrderStatusId == id);

            return View(findOrderStatus);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(OrderStatusModel modelOrderStatus)
        {
            List<OrderStatusModel> listOrderStatus = await dataAccessOrderStatus.OrderStatusViewData();

            OrderStatusModel findUpdatedOrderStatus = listOrderStatus.Single(OS => OS.OrderStatusId == modelOrderStatus.OrderStatusId);

            await TryUpdateModelAsync(findUpdatedOrderStatus);

            if (ModelState.IsValid)
            {
                await dataAccessOrderStatus.OrderStatusUpdateOrInsert(findUpdatedOrderStatus);

                return RedirectToAction("Index");
            }
            return View(findUpdatedOrderStatus);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessOrderStatus.OrderStatusDelete(id);

            return RedirectToAction("Index");
        }

    }
}
