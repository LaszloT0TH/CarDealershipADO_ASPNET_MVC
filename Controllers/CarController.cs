using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipASPNETMVC.Controllers
{
    public class CarController : Controller
    {
        private readonly IDataAccessCars dataAccessCars;
        private readonly IDataAccessShoppingCart dataAccessShoppingCart;

        public CarController(IDataAccessCars dataAccessCars,
                                IDataAccessShoppingCart dataAccessShoppingCart)
        {
            this.dataAccessCars = dataAccessCars;
            this.dataAccessShoppingCart = dataAccessShoppingCart;
        }

        /// <summary>
        /// Car 
        /// sold cars cannot be bought, therefore adding to the cart in the list is not possible (View "Index")
        /// az eladott autókat nem lehet megvennni, ezért a listában a kosárhoz adás nem lehetséges 
        /// </summary>
        /// <returns></returns>
        // View Car All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Cars";

            List<CarModel> ListCars = await dataAccessCars.CarsViewData();

            return await Task.Run(() => View("Index", ListCars));
        }

        // Car Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Cars Search";

            ViewBag.Fuel = await dataAccessCars.FuelViewData();
            
            ViewBag.Gearbox = await dataAccessCars.GearboxViewData();
            
            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CarModel searchedCar = new CarModel();

            await TryUpdateModelAsync(searchedCar);

            List<CarModel> listCarSearchResult = await dataAccessCars.CarsViewData(searchedCar);

            return await Task.Run(() => View("Index", listCarSearchResult));

        }

        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Cars Create";

            ViewBag.Fuel = await dataAccessCars.FuelViewData();

            ViewBag.Gearbox = await dataAccessCars.GearboxViewData();

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post()
        {
            if (ModelState.IsValid)
            {
                CarModel insertedCar = new CarModel();

                await TryUpdateModelAsync(insertedCar);

                await dataAccessCars.CarsUpdateOrInsert(insertedCar);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Cars Edit";

            ViewBag.Fuel = await dataAccessCars.FuelViewData();

            ViewBag.Gearbox = await dataAccessCars.GearboxViewData();

            List<CarModel> listCar = await dataAccessCars.CarsViewData();

            CarModel findCar = listCar.Single(car => car.CarId == id);

            return View(findCar);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CarModel modelCar)
        {
            List<CarModel> listCar = await dataAccessCars.CarsViewData();

            CarModel findUpdatedCar = listCar.Single(car => car.CarId == modelCar.CarId);

            await TryUpdateModelAsync(findUpdatedCar);

            if (ModelState.IsValid)
            {
                await dataAccessCars.CarsUpdateOrInsert(findUpdatedCar);

                return RedirectToAction("Index");
            }
            return View(findUpdatedCar);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCars.CarsDelete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Sale(int id)
        {
            ShoppingCartModel newShoppingCartItem = new ShoppingCartModel();

            newShoppingCartItem.UserId = GlobalData.UserId;

            newShoppingCartItem.ProductId = id;

            newShoppingCartItem.Quantity = 1;

            await dataAccessShoppingCart.ShoppingCartUpdateOrInsert(newShoppingCartItem);

            return RedirectToAction("Index");

        }
    }
}
