using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace CarDealershipASPNETMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDataAccessCustomer dataAccessCustomer;
        private readonly IDataAccessLogin dataAccessLogin;
        private readonly IDataAccess_HelpQuery dataAccess_HelpQuery;

        public CustomerController(IDataAccessCustomer dataAccessCustomer,
                                    IDataAccessLogin dataAccessLogin,
                                    IDataAccess_HelpQuery dataAccess_HelpQuery)
        {
            this.dataAccessCustomer = dataAccessCustomer;
            this.dataAccessLogin = dataAccessLogin;
            this.dataAccess_HelpQuery = dataAccess_HelpQuery;
        }

        /// <summary>
        /// Customer
        /// </summary>
        /// <returns></returns>
        // View Customer All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Customers";

            List<CustomerModel> ListCustomers = await dataAccessCustomer.CustomersViewData();

            return await Task.Run(() => View("Index", ListCustomers));
        }

        // Customer Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Customers Search";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();
            
            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CustomerModel searchedCustomer = new CustomerModel();

            await TryUpdateModelAsync(searchedCustomer);

            List<CustomerModel> listCustomersSearchResult = await dataAccessCustomer.CustomersViewData(searchedCustomer);

            return await Task.Run(() => View("Index", listCustomersSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Customers Create";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();

            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(CustomerInsertModel customerInsertModel)
        {
            if (ModelState.IsValid)
            {
                CustomerModel insertedCustomer = new CustomerModel();

                await TryUpdateModelAsync(insertedCustomer);

                await dataAccessCustomer.CustomersUpdateOrInsert(insertedCustomer);

                // If you don't have a user ID yet, you will be redirected to the login page, if you have one, you will be redirected to the Customer menu
                // Wenn Sie noch keine Benutzer-ID haben, werden Sie auf die Anmeldeseite weitergeleitet, wenn Sie eine haben, werden Sie zum Kundenmenü weitergeleitet
                // Ha még nincs user Id-ja, akkor bejelentkező oldal, ha van akkor a Customer menüpontra irányítja át
                if (GlobalData.UserId == 0)
                {
                    // inserts the email address into the tbl Login table
                    // fügt die E-Mail-Adresse in die tbl-Login-Tabelle ein
                    // az emailcímet beszúrja a tblLogin táblázatba
                    await dataAccessLogin.EmailUpload(insertedCustomer.Email);

                    return RedirectToAction("Edit","Login");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            // In case of incorrect filling, if you do not have a user ID, you will be redirected to the registration page, if you have one, you will be redirected to the Create menu item
            // Falls Sie keine Benutzer-ID haben, werden Sie bei einer falschen Eingabe auf die Registrierungsseite weitergeleitet, falls Sie eine haben, werden Sie zum Menüpunkt Erstellen weitergeleitet
            // Hibás kitöltés esetén a még nincs user Id-ja, akkor regisztrációs oldalra, ha van akkor a Create menüpontra irányítja át
            if (GlobalData.UserId == 0)
            {
                return RedirectToAction("Create", "Customer");
            }
            else
            {
                return View();
            }          
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Customers Edit";

            ViewBag.Sex = await dataAccess_HelpQuery.SexViewData();

            ViewBag.Country = await dataAccess_HelpQuery.CountryViewData();

            List<CustomerModel> listCustomers = await dataAccessCustomer.CustomersViewData();

            CustomerModel findCustomer = listCustomers.Single(customer => customer.CustomerId == id);

            return View(findCustomer);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CustomerModel modelCustomer)
        {
            List<CustomerModel> listCustomers = await dataAccessCustomer.CustomersViewData();

            CustomerModel findUpdatedCustomer = listCustomers.Single(customer => customer.CustomerId == modelCustomer.CustomerId);

            await TryUpdateModelAsync(findUpdatedCustomer);

            if (ModelState.IsValid)
            {
                await dataAccessCustomer.CustomersUpdateOrInsert(findUpdatedCustomer);

                return RedirectToAction("Index");
            }
            return View(findUpdatedCustomer);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCustomer.CustomersDelete(id);

            return RedirectToAction("Index");
        }

    }
}
