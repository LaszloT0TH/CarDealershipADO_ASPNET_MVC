using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarDealershipASPNETMVC.Controllers
{
    public class SettingsCountryController : Controller
    {
        private readonly IDataAccessCountry dataAccessCountry;

        public SettingsCountryController(IDataAccessCountry dataAccessCountry)
        {
            this.dataAccessCountry = dataAccessCountry;
        }

        // View Country All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Countries Settings";

            List<CountryModel> ListCountries = await dataAccessCountry.CountrysViewData();

            return await Task.Run(() => View("Index", ListCountries));
        }

        // Country Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Countries Search";

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CountryModel searchedCountry = new CountryModel();

            await TryUpdateModelAsync(searchedCountry);

            List<CountryModel> listCountrySearchResult = await dataAccessCountry.CountrysViewData(searchedCountry);

            return await Task.Run(() => View("Index", listCountrySearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Countries Create";

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                CountryModel insertedCountry = new CountryModel();

                await TryUpdateModelAsync(insertedCountry);

                await dataAccessCountry.CountrysUpdateOrInsert(insertedCountry);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Countries Edit";

            List<CountryModel> listCar = await dataAccessCountry.CountrysViewData();

            CountryModel findCountry = listCar.Single(Country => Country.CountryId == id);

            return View(findCountry);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CountryModel modelCountry)
        {
            List<CountryModel> listCountry = await dataAccessCountry.CountrysViewData();

            CountryModel findUpdatedCountry = listCountry.Single(Country => Country.CountryId == modelCountry.CountryId);

            await TryUpdateModelAsync(findUpdatedCountry);

            if (ModelState.IsValid)
            {
                await dataAccessCountry.CountrysUpdateOrInsert(findUpdatedCountry);

                return RedirectToAction("Index");
            }
            return View(findUpdatedCountry);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCountry.CountrysDelete(id);

            return RedirectToAction("Index");
        }

    }
}
