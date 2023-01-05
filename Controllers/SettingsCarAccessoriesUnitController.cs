using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsCarAccessoriesUnitController : Controller
    {
        private readonly IDataAccessCarAccessoriesUnit dataAccessCarAccessoriesUnit;

        public SettingsCarAccessoriesUnitController(IDataAccessCarAccessoriesUnit dataAccessCarAccessoriesUnit)
        {
            this.dataAccessCarAccessoriesUnit = dataAccessCarAccessoriesUnit;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Unit Settings";

            List<CarAccessoriesUnitModel> ListCAU = await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsViewData();

            return await Task.Run(() => View("Index", ListCAU));
        }

        // Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Unit Settings Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CarAccessoriesUnitModel searchedCAU = new CarAccessoriesUnitModel();

            await TryUpdateModelAsync(searchedCAU);

            List<CarAccessoriesUnitModel> listCAUSearchResult = await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsViewData(searchedCAU);

            return await Task.Run(() => View("Index", listCAUSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Unit Settings Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                CarAccessoriesUnitModel insertedCAU = new CarAccessoriesUnitModel();

                await TryUpdateModelAsync(insertedCAU);

                await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsUpdateOrInsert(insertedCAU);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Unit Settings Edit";

            List<CarAccessoriesUnitModel> listCAU = await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsViewData();

            CarAccessoriesUnitModel findCAU = listCAU.Single(carAU => carAU.CAUId == id);

            return View(findCAU);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CarAccessoriesUnitModel CAu)
        {
            List<CarAccessoriesUnitModel> listCAU = await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsViewData();

            CarAccessoriesUnitModel findUpdatedCAU = listCAU.Single(carAU => carAU.CAUId == CAu.CAUId);

            await TryUpdateModelAsync(findUpdatedCAU);

            if (ModelState.IsValid)
            {
                await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsUpdateOrInsert(findUpdatedCAU);

                return RedirectToAction("Index");
            }
            return View(findUpdatedCAU);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCarAccessoriesUnit.CarAccessoriesUnitsDelete(id);

            return RedirectToAction("Index");
        }

    }
}
