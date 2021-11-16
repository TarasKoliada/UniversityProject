using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation;
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
        Customer _customer;
        public void ShowMenu()
        {
            int choise, siteNavigation;
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
                        IRegistrable<Customer> registrable = new CustomerRegister();
                        _customer = registrable.Register();
                        break;
                    case 2:
                        ILogger<Customer> logger = new CustomerLogin();
                        _customer = logger.Login();
                        break;
                }
                var site = ChooseSite();

                var order = new CustomerOrder(_customer, site);

                Console.WriteLine("  1) Create order");
                Console.WriteLine("  2) Show my orders");
                Console.WriteLine("  3) Show my profile info");
                //Console.WriteLine("  4) Show my profile info");
                Console.Write("Your choise: ");

                 parsed = int.TryParse(Console.ReadLine(), out siteNavigation);
                 if (parsed)
                 {
                     switch (siteNavigation)
                     {
                         case 1:
                             order.Create();
                             break;
                     }
                 }
            }
            else
            {
                Console.WriteLine("\nSelect the first or second option\n");
                ShowMenu();
            }
           
        }
        private Site ChooseSite()
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
