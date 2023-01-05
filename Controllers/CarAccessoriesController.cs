using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Global;
using CarDealershipASPNETMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace CarDealershipASPNETMVC.Controllers
{
    public class CarAccessoriesController : Controller
    {
        private readonly IDataAccessCarAccessories dataAccessCarAccessories;
        private readonly IDataAccessShoppingCart dataAccessShoppingCart;
        private readonly IWebHostEnvironment webHostEnvironment;

        // a program.cs "builder.Services.AddSingleton<IDataAccess, DataAccess>();" sora hívódik meg, ami beállítja az Interface értékét
        public CarAccessoriesController(IDataAccessCarAccessories dataAccessCarAccessories, 
                                        IWebHostEnvironment webHostEnvironment,
                                        IDataAccessShoppingCart dataAccessShoppingCart)
        {
            this.dataAccessCarAccessories = dataAccessCarAccessories;
            this.webHostEnvironment = webHostEnvironment;
            this.dataAccessShoppingCart = dataAccessShoppingCart;
        }

        /// <summary>
        /// Car Accessories
        /// </summary>
        /// <returns></returns>
        // View Car Accessories All Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Car Accessories";
            
            List<CarAccessoriesModel> ListCarAccessories = await dataAccessCarAccessories.CarAccessoriesViewData();

            return await Task.Run(() => View("Index", ListCarAccessories));
        }

        // Car Accessories Search Dynamic SQL
        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Get()
        {
            ViewData["Title"] = "Car Accessories Search";

            ViewBag.CarAccessoriesProductGroup = await dataAccessCarAccessories.CarAccessoriesProductGroupViewData();
            //ViewBag.CarAccessoriesProductGroup = new SelectList(await dataAccess.CarAccessoriesProductGroupViewData(), "CAPGId", "CAPGName");

            ViewBag.CarAccessoriesUnit = await dataAccessCarAccessories.CarAccessoriesUnitViewData();
            //ViewBag.CarAccessoriesUnit = new SelectList(await dataAccess.CarAccessoriesUnitViewData(), "CAUId", "UnitName");
            
            return View();
        }
        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search_Post()
        {
            CarAccessoriesModel searchedCarAccessories = new CarAccessoriesModel();
            // átveszi a kitöltött oldalról a paramétereket
            await TryUpdateModelAsync(searchedCarAccessories);

            List<CarAccessoriesModel> listCarAccessoriesSearchResult =  await dataAccessCarAccessories.CarAccessoriesViewData(searchedCarAccessories);

            //return RedirectToAction("Search_Result");

            return await Task.Run(() => View("Index", listCarAccessoriesSearchResult));

        }

        // A HttpGet az oldal hívását teszi lehetővé, az ott megadott adatok a HttpPost-ba kerülnek kattintáskor, létre kell hozni mindkettőt
        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Get()
        {
            ViewData["Title"] = "Car Accessories Create";
            
            ViewBag.CarAccessoriesProductGroup = await dataAccessCarAccessories.CarAccessoriesProductGroupViewData();

            ViewBag.CarAccessoriesUnit = await dataAccessCarAccessories.CarAccessoriesUnitViewData();

            return View(); 
        }         
        // ez történik a HttpGet-ben gyűjtott adatokkal
        // a konstrukrotban lehet paraméterként megadni a tulajdonságokat, amiket később fel lehet használni így kevesebb az írás
        // vagy közvetlenül az objektumot átadni
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(CarAccessoriesInsertModel carAccessoriesInserted)
        {
             // hibák esetén hagyom ezen az oldalon
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(carAccessoriesInserted);

                CarAccessoriesModel insertedCarAccessories = new CarAccessoriesModel();
                
                // átveszi a kitöltött oldalról a paramétereket
                await TryUpdateModelAsync(insertedCarAccessories);

                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                // Speichern Sie den Dateinamen in der PhotoPath-Eigenschaft des Mitarbeiterobjekts
                // die in der Employees-Datenbanktabelle gespeichert wird
                // Tárolja a fájl nevét az alkalmazotti objektum PhotoPath tulajdonságában
                // amely az Alkalmazottak adatbázistáblájába kerül mentésre
                insertedCarAccessories.PhotoPath = uniqueFileName;
                //DataAccess dataAccess = new DataAccess();

                await dataAccessCarAccessories.CarAccessoriesUpdateOrInsert(insertedCarAccessories);

                return RedirectToAction("Index");
            }
            return View();
        }

        // Update
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Get(int id)
        {
            ViewData["Title"] = "Car Accessories Edit";

            ViewBag.CarAccessoriesProductGroup = await dataAccessCarAccessories.CarAccessoriesProductGroupViewData();
           
            ViewBag.CarAccessoriesUnit = await dataAccessCarAccessories.CarAccessoriesUnitViewData();

            List<CarAccessoriesModel> listCarAccessories = await dataAccessCarAccessories.CarAccessoriesViewData();
            
            CarAccessoriesModel findCarAccessories = listCarAccessories.Single(car => car.CAId == id);

            CarAccessoriesEditModel carAccessoriesEditModel = new CarAccessoriesEditModel()
            {
                CAId = Convert.ToInt32(findCarAccessories.CAId),
                ProductName = Convert.ToString(findCarAccessories.ProductName),
                CAPGName = Convert.ToString(findCarAccessories.CAPGName),
                QuantityOfStock = Convert.ToInt32(findCarAccessories.QuantityOfStock),
                MinimumStockQuantity = Convert.ToInt32(findCarAccessories.MinimumStockQuantity),
                NetSellingPrice = Convert.ToDouble(findCarAccessories.NetSellingPrice),
                SalesUnit = Convert.ToDouble(findCarAccessories.SalesUnit),
                UnitPrice = Convert.ToDouble(findCarAccessories.UnitPrice),
                UnitName = Convert.ToString(findCarAccessories.UnitName),
                LastUpdateTime = Convert.ToDateTime(findCarAccessories.LastUpdateTime),
                Brand = Convert.ToString(findCarAccessories.Brand),
                CreationDate = Convert.ToDateTime(findCarAccessories.CreationDate),
                Description = Convert.ToString(findCarAccessories.Description),
                Version = Convert.ToInt32(findCarAccessories.Version),
                ExistingPhotoPath = Convert.ToString(findCarAccessories.PhotoPath)
            };

            return View(carAccessoriesEditModel);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit_Post(CarAccessoriesEditModel modelCarAccessories)
        {
            // A biztonság érdekében, hogy ne lehessen feltörni az oldalt
            List<CarAccessoriesModel> listCarAccessories = await dataAccessCarAccessories.CarAccessoriesViewData();
            
            CarAccessoriesModel findUpdatedCarAccessories = listCarAccessories.Single(car => car.CAId == modelCarAccessories.CAId);

            // átveszi a kitöltött oldalról a paramétereket, a tulterhelt változatát kell használni a biztonság
            // érdekében egy string tömbben történik a kizárás meghatározott tulajdonságok alapján null értékek nélkül
            // a tömbben foglaltak tartoznak bele
            //      A Fiddler programmal lehetne feltörni weboldalakat
            //await TryUpdateModelAsync(findUpdatedCarAccessories, null, null, new string[] { "" });
            // lehet még hasnálni Edit_Post([Bind(Include="Id, Name" )]CarAccessoriesModel carAccessories) konstruktort is "Exclude" paraméterrel is
            await TryUpdateModelAsync(findUpdatedCarAccessories);

            


            // If the user wants to change the photo, a new photo will be
            // uploaded and the Photo property on the model object receives
            // the uploaded photo. If the Photo property is null, user did
            // not upload a new photo and keeps his existing photo
            if (modelCarAccessories.Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (modelCarAccessories.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", modelCarAccessories.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object which will be
                // eventually saved in the database
                findUpdatedCarAccessories.PhotoPath = ProcessUploadedFile(modelCarAccessories);
            }


            // hibák esetén hagyom ezen az oldalon
            if (ModelState.IsValid)
            {
                await dataAccessCarAccessories.CarAccessoriesUpdateOrInsert(findUpdatedCarAccessories);

                return RedirectToAction("Index");
            }
            return View(modelCarAccessories);
        }

        private string ProcessUploadedFile(CarAccessoriesInsertModel modelCarAccessories)
        {
            string uniqueFileName = null;
 
            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            // Wenn die Photo-Eigenschaft des eingehenden Modellobjekts nicht null ist, dann der Benutzer
            // hat ein Bild zum Hochladen ausgewählt.
            // Ha a bejövő modellobjektum Photo tulajdonsága nem null, akkor a felhasználó
            // kiválasztotta a feltöltendő képet.
            if (modelCarAccessories.Photo != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                // Das Bild muss in den Bilderordner in wwwroot hochgeladen werden
                // Um den Pfad des wwwroot-Ordners zu erhalten, verwenden wir das inject
                // HostingEnvironment-Dienst, bereitgestellt von ASP.NET Core
                // A képet fel kell tölteni a wwwroot images mappájába
                // A wwwroot mappa elérési útjának lekéréséhez az injekciót használjuk
                // Az ASP.NET Core által biztosított tárhelykörnyezeti szolgáltatás
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                // Um sicherzustellen, dass der Dateiname eindeutig ist, hängen wir eine neue an
                // GUID-Wert und ein Unterstrich für den Dateinamen
                // Annak érdekében, hogy a fájlnév egyedi legyen, egy újat fűzünk hozzá
                // GUID érték és és aláhúzás a fájlnévhez
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelCarAccessories.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                // Verwenden Sie die CopyTo()-Methode, die von der IFormFile-Schnittstelle bereitgestellt wird
                // Kopieren Sie die Datei in den Ordner wwwroot/images
                // Használja az IFormFile felület által biztosított CopyTo() metódust
                // másolja a fájlt a wwwroot/images mappába
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelCarAccessories.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await dataAccessCarAccessories.CarAccessoriesDelete(id);

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
