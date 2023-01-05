using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsSpokenLanguesController : Controller
    {
        private readonly IDataAccessSpokenLangues dataAccessSpokenLangues;

        public SettingsSpokenLanguesController(IDataAccessSpokenLangues dataAccessSpokenLangues)
        {
            this.dataAccessSpokenLangues = dataAccessSpokenLangues;
        }

        // View SpokenLanguesModel All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Spoken Langues Settings";

            List<SpokenLanguesModel> ListSpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();

            return await Task.Run(() => View("Index", ListSpokenLangues));
        }

        // SpokenLangues Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Spoken Langues Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            SpokenLanguesModel searchedSpokenLangues = new SpokenLanguesModel();

            await TryUpdateModelAsync(searchedSpokenLangues);

            List<SpokenLanguesModel> listSpokenLanguesResult = await dataAccessSpokenLangues.SpokenLanguesViewData(searchedSpokenLangues);

            return await Task.Run(() => View("Index", listSpokenLanguesResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Spoken Langues Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                SpokenLanguesModel insertedSpokenLangues = new SpokenLanguesModel();

                await TryUpdateModelAsync(insertedSpokenLangues);

                await dataAccessSpokenLangues.SpokenLanguesUpdateOrInsert(insertedSpokenLangues);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Spoken Langues Edit";

            List<SpokenLanguesModel> listSpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();
           
            SpokenLanguesModel findSpokenLangues = listSpokenLangues.Single(sp => sp.SpokenLanguesId == id);

            return View(findSpokenLangues);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(SpokenLanguesModel spokenLangues)
        {
            List<SpokenLanguesModel> listSpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();

            SpokenLanguesModel findSpokenLangues = listSpokenLangues.Single(sp => sp.SpokenLanguesId == spokenLangues.SpokenLanguesId);


            await TryUpdateModelAsync(findSpokenLangues);

            if (ModelState.IsValid)
            {
                await dataAccessSpokenLangues.SpokenLanguesUpdateOrInsert(findSpokenLangues);

                return RedirectToAction("Index");
            }
            return View(findSpokenLangues);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessSpokenLangues.SpokenLanguesDelete(id);

            return RedirectToAction("Index");
        }

    }
}
