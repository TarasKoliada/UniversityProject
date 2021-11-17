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
        public static Site ChooseSite()
        {
            IDataProvider<Site> provider = new SiteDataProvider();

            int iterator = 0;
            int siteId;

            Console.WriteLine("\nChoose site to visit: ");

            foreach (var sites in provider.GetContext())
            {
                Console.WriteLine($"  {++iterator}) {sites.Name}");
            }

            Console.Write("Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out siteId);

            if (parsed)
            {
                var site = GetSiteById(siteId);

                if (site != null)
                {
                    Console.WriteLine($"\nGreat choise! Welcome to {site.Name}");
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
            IDataProvider<Site> provider = new SiteDataProvider();
            return provider.GetContext().Count();
        }

        public static void GetAllSites()
        {
            IDataProvider<Site> provider = new SiteDataProvider();
            foreach (var item in provider.GetContext())
            {
                Console.WriteLine($"{item.Id}) {item.Name}");
            }
            
        }
        public static Site GetSiteById(int id)
        {
            IDataProvider<Site> provider = new SiteDataProvider();
            foreach (var site in provider.GetContext())
            {
                if (site.Id == id)
                {
                    return site;
                }
            }
            return null;
        }

        
    }
}
