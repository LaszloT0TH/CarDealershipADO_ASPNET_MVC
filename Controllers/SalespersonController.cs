using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipASPNETMVC.Controllers
{
    public class SalespersonController : Controller
    {
        private readonly IDataAccessSalesperson dataAccessSalesperson;
        private readonly IDataAccess_HelpQuery dataAccess_HelpQuery;
        private readonly IDataAccessSpokenLangues dataAccessSpokenLangues;
        private readonly IDataAccessLogin dataAccessLogin;

        public SalespersonController(IDataAccessSalesperson dataAccessSalesperson,
                                IDataAccess_HelpQuery dataAccess_HelpQuery,
                                IDataAccessSpokenLangues dataAccessSpokenLangues,
                                IDataAccessLogin dataAccessLogin)
        {
            this.dataAccessSalesperson = dataAccessSalesperson;
            this.dataAccess_HelpQuery = dataAccess_HelpQuery;
            this.dataAccessSpokenLangues = dataAccessSpokenLangues;
            this.dataAccessLogin = dataAccessLogin;
        }

        /// <summary>
        /// Customer
        /// </summary>
        /// <returns></returns>
        // View Car Accessories All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Salespersons";
            
            List<SalespersonModel> ListSalespersons = await dataAccessSalesperson.SalespersonsViewData();

            return await Task.Run(() => View("Index", ListSalespersons));
        }

        // Customer Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Salespersons Search";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();

            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            ViewBag.SpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();

            ViewBag.Manager = await dataAccess_HelpQuery.ManagerViewData();

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            SalespersonModel searchedSalesperson = new SalespersonModel();

            await TryUpdateModelAsync(searchedSalesperson);

            List<SalespersonModel> listSalespersonsSearchResult = await dataAccessSalesperson.SalespersonsViewData(searchedSalesperson);

            return await Task.Run(() => View("Index", listSalespersonsSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Salespersons Create";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();

            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            ViewBag.SpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();

            ViewBag.Manager = await dataAccess_HelpQuery.ManagerViewData();

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                SalespersonModel insertedSalesperson = new SalespersonModel();

                await TryUpdateModelAsync(insertedSalesperson);

                await dataAccessSalesperson.SalespersonsUpdateOrInsert(insertedSalesperson);

                // inserts the email address into the tbl Login table
                // fügt die E-Mail-Adresse in die tbl-Login-Tabelle ein
                // az emailcímet beszúrja a tblLogin táblázatba
                await dataAccessLogin.EmailUpload(insertedSalesperson.Email);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Salespersons Edit";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();

            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            ViewBag.SpokenLangues = await dataAccessSpokenLangues.SpokenLanguesViewData();
            
            ViewBag.Manager = await dataAccess_HelpQuery.ManagerViewData();

            List<SalespersonModel> listSalespersons = await dataAccessSalesperson.SalespersonsViewData();

            SalespersonModel findSalesperson = listSalespersons.Single(salesperson => salesperson.SalesId == id);

            return View(findSalesperson);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(SalespersonModel modelSalesperson)
        {
            List<SalespersonModel> listSalespersons = await dataAccessSalesperson.SalespersonsViewData();

            SalespersonModel findUpdatedSalesperson = listSalespersons.Single(salesperson => salesperson.SalesId == modelSalesperson.SalesId);

            await TryUpdateModelAsync(findUpdatedSalesperson);

            if (ModelState.IsValid)
            {
                await dataAccessSalesperson.SalespersonsUpdateOrInsert(findUpdatedSalesperson);

                return RedirectToAction("Index");
            }
            return View(findUpdatedSalesperson);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessSalesperson.SalespersonsDelete(id);

            return RedirectToAction("Index");
        }

    }
}
