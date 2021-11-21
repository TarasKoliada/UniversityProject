﻿using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation;
using FoodOrderingDB.Business_Logic.Implementation.Register;
using FoodOrderingDB.Business_Logic.Remove;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.User_Interface
{
    class AdminSiteMenu
    {
        Administrator _admin;
        public AdminSiteMenu()
        {
            Login();
        }
        public void ShowMenu()
        {
            int choise;
            Console.WriteLine("1) Add a new Employee to the Site");
            Console.WriteLine("2) Get all the Site Employee info");
            Console.WriteLine("3) Remove an employee from the site");
            Console.WriteLine("4) Get my profile info");
            Console.WriteLine("5) Get dish ratings");
            Console.WriteLine("6) Log out");
            Console.Write("Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out choise);
            if (parsed)
            {
                switch (choise)
                {
                    case 1:
                        Console.Clear();
                        IRegistrable<Employee> registrable = new EmployeeRegister(_admin.SiteId);
                        registrable.Register();
                        ShowMenu();
                        break;
                    case 2:
                        Console.Clear();
                        StaticEmployeeInfo.GetSiteEmployees(_admin.Site);
                        ShowMenu();
                        break;
                    case 3:
                        Console.Clear();
                        if (_admin.Preferences == true)
                        {
                            EmployeeRemover remover = new EmployeeRemover(_admin);
                            remover.Remove();
                        }
                        else
                        {
                            Console.WriteLine("You have not preferences to remove employee");
                        }
                        ShowMenu();
                        break;
                    case 4:
                        Console.Clear();
                        StaticAdminInfo.GetFullInfo(_admin);
                        ShowMenu();
                        break;
                    case 6:
                        _admin = null;
                        Console.Clear();
                        break;
                }
            }
        }
        private void Login()
        {
            ILogger<Administrator> logger = new AdminLogin();
            _admin = logger.Login();
        }
    }
}
