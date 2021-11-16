using FoodOrderingDB.Abstractions;
using FoodOrderingDB.User_Interface;
using FoodOrderingDB.User_Interface.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerSiteMenu siteMenu = new CustomerSiteMenu();
            siteMenu.ShowMenu();
            var customer = new Customer();
        }
        
    }
}
