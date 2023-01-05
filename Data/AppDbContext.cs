using CarDealershipASPNETMVC.Models;
using Microsoft.AspNet.Identity.CoreCompat;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipASPNETMVC.Data
{
    public class AppDbContext :  DbContext
    {
        //kattintsunk az osztálynévre és legenerálja az "option" paraméterrel a konstruktort "CTRL + ."
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        // az Interface csak a támogatott műveletek halmazát jelöli ki, amelyeket az osztály tartalmaz
        public DbSet<IDataAccess> Alldata { get; set; }

        // felülírhatóak a beépített metódusok
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // a "<>" jel valamilyen adatok halmazát jelenti, amit modellként határozunk meg
        public DbSet<CarAccessoriesModel> CarAccessories { get; set; }

        // hozzá kell adnunk a Program.cs-hez a "builder.Services.AddControllersWithViews();" után

    }

}
