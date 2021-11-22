﻿using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
            int choise;
            Console.WriteLine("Are you a new User or already have an account?\n");
            Console.WriteLine("  1) I'm a new User. Sign Up");
            Console.WriteLine("  2) I already have an account. Log In");
            Console.WriteLine("  3) Exit");
            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out choise);
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
                        ILogger<Customer> logger = new CustomerLogin();
                        _customer = logger.Login();
                        break;
                    case 3:
                        var navigation = new SiteNavigation();
                        navigation.ShowMenu();
                        break;
                }
                Console.Clear();
                Console.Write($"\nSuccess! Welcome ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{_customer.FirstName}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSelect the first or second option\n");
                Console.ResetColor();
                EntryMenu();
            }
        }
        private void ProfileMenu()
        {
            int choise;
            Console.WriteLine("\n--------MENU------------");
            Console.WriteLine("\n1) Go to the site");
            Console.WriteLine("2) Show my profile info");
            Console.WriteLine("3) Logout\n");
            Console.WriteLine("------------------------");
            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out choise);
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
                        StaticCustomerInfo.GetFullInfo(_customer);
                        ChangePassMenu();
                        ProfileMenu();
                        break;
                    case 3:
                        _customer = null;
                        EntryMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Input!");
                        Console.ResetColor();
                        ProfileMenu();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input! ");
                Console.ResetColor();
                ProfileMenu();
            }
        }

        private void ChangePassMenu()
        {
            Console.WriteLine("\n  1) Change password");
            Console.WriteLine("  2) Turn back");
            Console.Write("  Your choise: ");
            int choiseProfile;
            var parsed = int.TryParse(Console.ReadLine(), out choiseProfile);
            if (parsed)
            {
                switch (choiseProfile)
                {
                    case 1:
                        StaticCustomerInfo.ChangePassword();
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input");
                        Console.ResetColor();
                        break;
                }
            }
            
        }
        private void ChooseSiteMenu()
        {
             _site = StaticSiteInfo.ChooseSite();
        }
        private void EnteredSiteMenu()
        {
            int siteNavigation;

            var order = new CustomerOrder(_customer, _site);
            Console.WriteLine("\n\n--------MENU------------");
            Console.WriteLine("\n 1) Create order");
            Console.WriteLine(" 2) Show my orders");
            Console.WriteLine(" 3) Leave a review about the dish");
            Console.WriteLine(" 4) Show site info");
            Console.WriteLine(" 5) Turn back\n");
            Console.WriteLine("------------------------");
            Console.Write("\nYour choise: ");

            var parsed = int.TryParse(Console.ReadLine(), out siteNavigation);
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
                        break;
                    case 4:
                        StaticSiteInfo.GetInfo(_site);
                        EnteredSiteMenu();
                        break;
                    case 5:
                        ProfileMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Input!");
                        Console.ResetColor();
                        EnteredSiteMenu();
                        break;
                }
            }
        }
    }
}
