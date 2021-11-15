using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.User_Interface
{
    class CustomerSiteMenu
    {
        public void ShowMenu()
        {
            int choise;
            Console.WriteLine("Are you a new User or already have an account?");
            Console.WriteLine("  1) I'm a new User. Sign Up");
            Console.WriteLine("  2) I already have an account. Log In");
            Console.Write("Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out choise);
            if (parsed && choise <= 2 && choise >= 1)
            {
                switch (choise)
                {
                    case 1:
                        IRegistrable registrable = new CustomerRegister();
                        registrable.Register();
                        break;
                    case 2:
                        ILogger logger = new CustomerLogin();
                        logger.Login();
                        break;
                }
                ChooseSite();
            }
            else
            {
                Console.WriteLine("\nSelect the first or second option\n");
                ShowMenu();
            }
           
        }
        private void ChooseSite()
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
                var site = GetSite(siteId);

                if (site != null)
                {
                    Console.WriteLine($"\n  Great choise! Welcome to {site.Name}");
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

            
        }
        private Site GetSite(int id)
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
