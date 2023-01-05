using CarDealershipASPNETMVC.Data;
using Microsoft.AspNet.Identity.CoreCompat;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 

// Add services to the container.
builder.Services.AddControllersWithViews();



// hozz� kell adnunk a n�vt�rrel egy�tt �s az opci�kat, mert �gy szeretn�m haszn�lni mint egy sql server adatb�zisban
// ezt az AppDbContext
// A met�dus haszn�lata az sql szerver egy kapcsolati karakterl�ncot vesz fel
// j� gyakorlat, hogy ezt az "appsettings.json"-ban t�rolom
// meghat�rozom az adatbank kiszolg�l�i kapcsolat t�pus�t "UseSqlServer"
builder.Services.AddDbContextPool<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringsDell")));





// a migr�ci�t meg kell csin�lni el�tte
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
//{
//    option.Password.RequiredLength = 128;
//    option.Password.RequiredUniqueChars = 3;
//});




// Az IEmployeeRepository fel�letnek 2 implement�ci�ja van. Honnan tudja az alkalmaz�s, hogy melyik implement�ci�t kell haszn�lnia.
// A v�lasz erre az Ind�t�si oszt�ly Startup.cs f�jlban tal�lhat�. A k�vetkez� k�dsorral a ASP.NET Core az SQLEmployeeRepository
// oszt�ly egy p�ld�ny�t biztos�tja, amikor az IEmployeeRepository egy p�ld�ny�t k�rik.
// Az�rt haszn�ljuk az AddScoped() met�dust, mert azt szeretn�nk, hogy a p�ld�ny �letben legyen,
// �s el�rhet� legyen az adott HTTP-k�r�s teljes hat�k�r�ben. Egy m�sik �j HTTP-k�r�shez az SQLEmployeeRepository oszt�ly �j p�ld�nya
// lesz megadva, �s az a HTTP-k�r�s teljes hat�k�r�ben el�rhet� lesz.
// AddSingleton �gy ett�l f�gg a "CarAccessoriesController"
// <IDataAccess, DataAccess> konstruktora lek�ri az "appsettings.json" ConnectionStrings �rt�k�t  
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
