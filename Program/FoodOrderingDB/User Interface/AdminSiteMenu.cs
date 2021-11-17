using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation;
using FoodOrderingDB.Business_Logic.Implementation.Register;
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
            int choise, siteNavigation;
            Console.WriteLine("1) Add a new Employee to the Site");
            Console.WriteLine("2) Get all the Site Employee");
            Console.WriteLine("3) Get my profile info");
            Console.Write("Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out choise);
            if (parsed)
            {
                switch (choise)
                {
                    case 1:
                        IRegistrable<Employee> registrable = new EmployeeRegister(_admin.SiteId);
                        registrable.Register();
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
