using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class StaticSiteInfo
    {
        private static IDataProvider<Site> provider = new SiteDataProvider();
        public static Site ChooseSite()
        {

            int iterator = 0;
            int siteId;

            Console.WriteLine("\nChoose site to visit: \n");

            foreach (var sites in provider.GetContext())
            {
                Console.Write($"  {++iterator})");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($" {sites.Name}");
                Console.ResetColor();
            }

            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out siteId);
            Console.Clear();

            if (parsed)
            {
                var site = GetSiteById(siteId);

                if (site != null)
                {
                    Console.Write($"\nGreat choise! Welcome to ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{site.Name}");
                    Console.ResetColor();
                    return site;
                }
                else
                {
                    Console.WriteLine("\nThere is no such site in the list, choose other");
                    ChooseSite();
                }
            }
            else
            {
                Console.WriteLine("You can only enter site Id's");
                ChooseSite();
            }
            return null;
        }

        public static int GetSitesCount()
        {
            
            return provider.GetContext().Count();
        }

        public static void GetAllSites()
        {
            foreach (var item in provider.GetContext())
            {
                Console.WriteLine($"{item.Id}) {item.Name}");
            }
            
        }
        public static Site GetSiteById(int id)
        {
            foreach (var site in provider.GetContext())
            {
                if (site.Id == id)
                {
                    return site;
                }
            }
            return null;
        }

        public static void ShowSiteMenu(Site site)
        {
            var menuCounter = 0;
            var dishCounter = 0;
            Console.WriteLine();
            foreach (var menuTypes in site.MenuType)
            {
                Console.WriteLine($"\n {++menuCounter}) {menuTypes.Name}");
                foreach (var dishes in menuTypes.Dish)
                {
                    Console.Write($"    {++dishCounter}. ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{dishes.Name} ");
                    Console.ResetColor();
                    Console.Write($" | Weight: {dishes.Weight} | ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" Price: {dishes.Price}$");
                    Console.ResetColor();
                }
            }
        }

        public static void GetInfo(Site site)
        {
            Console.WriteLine($"\n  Id: {site.Id}");
            Console.WriteLine($"  Name: {site.Name}");
            Console.WriteLine($"  Description: {site.Description}");
            Console.WriteLine($"  Location: {site.LocationAdress}");
            Console.WriteLine($"  Contact info: {site.ContactInfo}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Administrators: ");
            Console.ResetColor();
            foreach (var admin in site.Administrator)
            {
                Console.WriteLine($"  ID: {admin.Id} | Full Name: {admin.Name}  {admin.MiddleName}  {admin.Surname} |  Hotline: {admin.Contact}");
            }
        }

    }
}
