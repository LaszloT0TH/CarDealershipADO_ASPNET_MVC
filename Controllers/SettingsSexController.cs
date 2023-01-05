using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsSexController : Controller
    {
        private readonly IDataAccessSex dataAccessSex;

        public SettingsSexController(IDataAccessSex dataAccessSex)
        {
            this.dataAccessSex = dataAccessSex;
        }


        // View Sex Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Sex Settings";

            List<SexModel> ListSexs = await dataAccessSex.SexsViewData();

            return await Task.Run(() => View("Index", ListSexs));
        }

        // Sex Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Sex Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            SexModel searchedSex = new SexModel();

            await TryUpdateModelAsync(searchedSex);

            List<SexModel> listSexsSearchResult = await dataAccessSex.SexsViewData(searchedSex);

            return await Task.Run(() => View("Index", listSexsSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Sex Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                SexModel insertedSex = new SexModel();

                await TryUpdateModelAsync(insertedSex);

                await dataAccessSex.SexsUpdateOrInsert(insertedSex);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Sex Edit";

            List<SexModel> listSex = await dataAccessSex.SexsViewData();

            SexModel findSex = listSex.Single(car => car.SexId == id);

            return View(findSex);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(SexModel modelSex)
        {
            List<SexModel> listSex = await dataAccessSex.SexsViewData();

            SexModel findUpdatedSex = listSex.Single(car => car.SexId == modelSex.SexId);

            await TryUpdateModelAsync(findUpdatedSex);

            if (ModelState.IsValid)
            {
                await dataAccessSex.SexsUpdateOrInsert(findUpdatedSex);

                return RedirectToAction("Index");
            }
            return View(findUpdatedSex);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessSex.SexsDelete(id);

            return RedirectToAction("Index");
        }

    }
}
