using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsFuelController : Controller
    {
        private readonly IDataAccessFuel dataAccessFuel;

        public SettingsFuelController(IDataAccessFuel dataAccessFuel)
        {
            this.dataAccessFuel = dataAccessFuel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Fuel Settings";

            List<FuelModel> ListFuel = await dataAccessFuel.FuelsViewData();

            return await Task.Run(() => View("Index", ListFuel));
        }

        // Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Fuel Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            FuelModel searchedFuel = new FuelModel();

            await TryUpdateModelAsync(searchedFuel);

            List<FuelModel> listFuelSearchResult = await dataAccessFuel.FuelsViewData(searchedFuel);

            return await Task.Run(() => View("Index", listFuelSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Fuel Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                FuelModel insertedFuel = new FuelModel();

                await TryUpdateModelAsync(insertedFuel);

                await dataAccessFuel.FuelsUpdateOrInsert(insertedFuel);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Fuel Edit";

            List<FuelModel> listFuels = await dataAccessFuel.FuelsViewData();

            FuelModel findFuel = listFuels.Single(fuel => fuel.FuelID == id);

            return View(findFuel);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(FuelModel carFuel)
        {
            List<FuelModel> listFuel = await dataAccessFuel.FuelsViewData();

            FuelModel findUpdatedFuel = listFuel.Single(fuel => fuel.FuelID == carFuel.FuelID);

            await TryUpdateModelAsync(findUpdatedFuel);

            if (ModelState.IsValid)
            {
                await dataAccessFuel.FuelsUpdateOrInsert(findUpdatedFuel);

                return RedirectToAction("Index");
            }
            return View(findUpdatedFuel);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessFuel.FuelsDelete(id);

            return RedirectToAction("Index");
        }

    }
}
