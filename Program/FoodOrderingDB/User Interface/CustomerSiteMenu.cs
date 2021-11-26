using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using FoodOrderingDB.Business_Logic.Implementation.Register;

namespace FoodOrderingDB.User_Interface
{
    class CustomerSiteMenu
    {
        Customer _customer;
        Site _site;
        public void ShowMenu()
        {
            EntryMenu();
            ProfileMenu();   
        }
        private void EntryMenu()
        {
            Console.WriteLine("Are you a new User or already have an account?\n");
            Console.WriteLine("  1) I'm a new User. Sign Up");
            Console.WriteLine("  2) I already have an account. Log In");
            Console.WriteLine("  3) Exit");
            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out int choise);
            Console.Clear();
            if (parsed && choise <= 3 && choise >= 1)
            {
                switch (choise)
                {
                    case 1:
                        IRegistrable<Customer> registrable = new CustomerRegister();
                        _customer = registrable.Register();
                        break;
                    case 2:
                        ILogin<Customer> logger = new CustomerLogin();
                        _customer = logger.Login();
                        break;
                    case 3:
                        var navigation = new SiteNavigation();
                        navigation.ShowMenu();
                        break;
                }
                Console.Clear();
                Console.Write($"\nSuccess! Welcome ");
                WriteMessage.Write($"{_customer.FirstName}", ConsoleColor.Green, false);
            }
            else
            {
                WriteMessage.Write("\nSelect the first or second option\n", ConsoleColor.Red, false);
                EntryMenu();
            }
        }
        private void ProfileMenu()
        {
            Console.WriteLine("\n--------MENU------------");
            Console.WriteLine("\n1) Go to the site");
            Console.WriteLine("2) Show my profile info");
            Console.WriteLine("3) Logout\n");
            Console.WriteLine("------------------------");
            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out int choise);
            Console.Clear();
            if (parsed)
            {
                switch (choise)
                {
                    case 1:
                        ChooseSiteMenu();
                        EnteredSiteMenu();
                        break;
                    case 2:
                        StaticCustomerInfo.GetFullInfo(_customer.Id);
                        ChangePassMenu();
                        ProfileMenu();
                        break;
                    case 3:
                        _customer = null;
                        EntryMenu();
                        break;
                    default:
                        WriteMessage.Write("Wrong Input!", ConsoleColor.Red, false);
                        ProfileMenu();
                        break;
                }
            }
            else
            {
                WriteMessage.Write("Wrong input! ", ConsoleColor.Red, false);
                ProfileMenu();
            }
        }

        private void ChangePassMenu()
        {
            Console.WriteLine("\n  1) Change password");
            Console.WriteLine("  2) Turn back");
            Console.Write("  Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out int choiseProfile);
            switch (choiseProfile)
            {
                case 1:
                    StaticCustomerInfo.ChangePassword(_customer.Id);
                    break;
                case 2:
                    Console.Clear();
                    break;
                default:
                    WriteMessage.Write("  Wrong input", ConsoleColor.Red);
                    break;
            }

        }
        private void ChooseSiteMenu()
        {
             _site = StaticSiteInfo.ChooseSite();
        }
        private void EnteredSiteMenu()
        {

            var order = new CustomerOrder(_customer, _site);
            Console.WriteLine("\n\n--------MENU------------");
            Console.WriteLine("\n 1) Create order");
            Console.WriteLine(" 2) Show my orders");
            Console.WriteLine(" 3) Leave a review about the dish");
            Console.WriteLine(" 4) Show site info");
            Console.WriteLine(" 5) Turn back\n");
            Console.WriteLine("------------------------");
            Console.Write("\nYour choise: ");

            var parsed = int.TryParse(Console.ReadLine(), out int siteNavigation);
            Console.Clear();
            if (parsed)
            {
                switch (siteNavigation)
                {
                    case 1:
                        order.Create();
                        EnteredSiteMenu();
                        break;
                    case 2:
                        order.GetOrders();
                        EnteredSiteMenu();
                        break;
                    case 3:
                        var rating = new RatingRegister(_customer, _site);
                        rating.Register();
                        EnteredSiteMenu();
                        break;
                    case 4:
                        StaticSiteInfo.GetInfo(_site.Id);
                        EnteredSiteMenu();
                        break;
                    case 5:
                        ProfileMenu();
                        break;
                    default:
                        WriteMessage.Write("Wrong Input!", ConsoleColor.Red);
                        EnteredSiteMenu();
                        break;
                }
            }
        }
    }
}
