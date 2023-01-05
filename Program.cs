using CarDealershipASPNETMVC.Data;
using Microsoft.AspNet.Identity.CoreCompat;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 

// Add services to the container.
builder.Services.AddControllersWithViews();



// hozzá kell adnunk a névtérrel együtt és az opciókat, mert úgy szeretném használni mint egy sql server adatbázisban
// ezt az AppDbContext
// A metódus használata az sql szerver egy kapcsolati karakterláncot vesz fel
// jó gyakorlat, hogy ezt az "appsettings.json"-ban tárolom
// meghatározom az adatbank kiszolgálói kapcsolat típusát "UseSqlServer"
builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringsDell")));





// a migrációt meg kell csinálni elõtte
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
//{
//    option.Password.RequiredLength = 128;
//    option.Password.RequiredUniqueChars = 3;
//});




// Az IEmployeeRepository felületnek 2 implementációja van. Honnan tudja az alkalmazás, hogy melyik implementációt kell használnia.
// A válasz erre az Indítási osztály Startup.cs fájlban található. A következõ kódsorral a ASP.NET Core az SQLEmployeeRepository
// osztály egy példányát biztosítja, amikor az IEmployeeRepository egy példányát kérik.
// Azért használjuk az AddScoped() metódust, mert azt szeretnénk, hogy a példány életben legyen,
// és elérhetõ legyen az adott HTTP-kérés teljes hatókörében. Egy másik új HTTP-kéréshez az SQLEmployeeRepository osztály új példánya
// lesz megadva, és az a HTTP-kérés teljes hatókörében elérhetõ lesz.
// AddSingleton így ettõl függ a "CarAccessoriesController"
// <IDataAccess, DataAccess> konstruktora lekéri az "appsettings.json" ConnectionStrings értékét  
builder.Services.AddScoped<IDataAccessCarAccessories, DataAccessCarAccessories>();
builder.Services.AddScoped<IDataAccessShoppingCart, DataAccessShoppingCart>();
builder.Services.AddScoped<IDataAccess_HelpQuery, DataAccess_HelpQuery>();
builder.Services.AddScoped<IDataAccessOrder, DataAccessOrder>();
builder.Services.AddScoped<IDataAccessOrderStatus, DataAccessOrderStatus>();
builder.Services.AddScoped<IDataAccessCustomer, DataAccessCustomer>();
builder.Services.AddScoped<IDataAccessLogin, DataAccessLogin>();
builder.Services.AddScoped<IDataAccessCars, DataAccessCars>();
builder.Services.AddScoped<IDataAccessCarAccessoriesProductGroup, DataAccessCarAccessoriesProductGroup>();
builder.Services.AddScoped<IDataAccessCarAccessoriesUnit, DataAccessCarAccessoriesUnit>();
builder.Services.AddScoped<IDataAccessCountry, DataAccessCountry>();
builder.Services.AddScoped<IDataAccessFuel, DataAccessFuel>();
builder.Services.AddScoped<IDataAccessGearbox, DataAccessGearbox>();
builder.Services.AddScoped<IDataAccessSex, DataAccessSex>();
builder.Services.AddScoped<IDataAccessSpokenLangues, DataAccessSpokenLangues>();
builder.Services.AddScoped<IDataAccessSalesperson, DataAccessSalesperson>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
