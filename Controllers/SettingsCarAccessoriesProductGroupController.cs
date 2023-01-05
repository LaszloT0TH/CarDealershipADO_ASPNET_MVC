using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsCarAccessoriesProductGroupController : Controller
    {
        private readonly IDataAccessCarAccessoriesProductGroup dataAccessCarAccessoriesProductGroup;

        public SettingsCarAccessoriesProductGroupController(IDataAccessCarAccessoriesProductGroup dataAccessCarAccessoriesProductGroup)
        {
            this.dataAccessCarAccessoriesProductGroup = dataAccessCarAccessoriesProductGroup;
        }

        // View All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Product Group Settings";

            List<CarAccessoriesProductGroupModel> ListCAPG = await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsViewData();

            return await Task.Run(() => View("Index", ListCAPG));
        }

        // Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Product Group Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CarAccessoriesProductGroupModel searchedCAPG = new CarAccessoriesProductGroupModel();

            await TryUpdateModelAsync(searchedCAPG);

            List<CarAccessoriesProductGroupModel> listCAPGSearchResult = await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsViewData(searchedCAPG);

            return await Task.Run(() => View("Index", listCAPGSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Product Group Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                CarAccessoriesProductGroupModel insertedCAPG = new CarAccessoriesProductGroupModel();

                await TryUpdateModelAsync(insertedCAPG);

                await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsUpdateOrInsert(insertedCAPG);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Product Group Edit";

            List<CarAccessoriesProductGroupModel> listCAPG = await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsViewData();

            CarAccessoriesProductGroupModel findCAPG = listCAPG.Single(carA => carA.CAPGId == id);

            return View(findCAPG);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CarAccessoriesProductGroupModel CAPG)
        {
            List<CarAccessoriesProductGroupModel> listCAPG = await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsViewData();

            CarAccessoriesProductGroupModel findUpdatedCAPG = listCAPG.Single(carA => carA.CAPGId == CAPG.CAPGId);

            await TryUpdateModelAsync(findUpdatedCAPG);

            if (ModelState.IsValid)
            {
                await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsUpdateOrInsert(findUpdatedCAPG);

                return RedirectToAction("Index");
            }
            return View(findUpdatedCAPG);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCarAccessoriesProductGroup.CarAccessoriesProductGroupsDelete(id);

            return RedirectToAction("Index");
        }
    }
}
