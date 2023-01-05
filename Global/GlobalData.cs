using CarDealershipASPNETMVC.Data;
using CarDealershipASPNETMVC.Models;
using System.Configuration;

namespace CarDealershipASPNETMVC.Global
{
    /// <summary>
    /// Short-term storage of global variables
    /// Kurzzeitspeicherung globaler Variablen
    /// Globális változók rövid távú tárolása
    /// </summary>
    public class GlobalData
    {
        private static DataAccessLogin dataAccessLogin = new DataAccessLogin();
        
        public static int UserId { get; set; }

        public static string UserAccess
        {
            get
            {
                if (UserId >= 1000)
                {
                    return "Customer";
                }
                else if(UserId < 1000 && UserId > 0)
                {
                    if (dataAccessLogin.IsManager(UserId).Result)
                    {
                        return "Manager";
                    }
                    else
                    {
                        return "Salesperson";
                    }
                }
                else
                {
                    return "Guest";
                }
            }
        }

        /// <summary>
        /// allows display of shopping cart states, initial value "in shopping cart", "Saved for later", "In transit", "shipped"
        /// ermöglicht die Anzeige von Warenkorbzuständen, Ausgangswert "Im Einkaufswagen", "Für später gespeichert", "Unterwegs", "Zugestellt"
        /// lehetővé teszi a bevásárlókosár állapotainak megjelenítését, kezdeti értéke "a bevásárlókosárban", "elmentve későbbre", "úton", "kiszállított"
        /// </summary>
        public static string ShoppingCartStatus { get; set; }


        /// <summary>
        /// initializes the contents of the shopping cart to "in the shopping cart" on first login
        /// initialisiert den Inhalt des Warenkorbs bei der ersten Anmeldung auf "im Warenkorb".
        /// első belépésnél a bevásárlói kosár tartalmát inicializálja "a bevásárlókosárban" értékre
        /// </summary>
        public static bool First_Entry = true;

    }
}
