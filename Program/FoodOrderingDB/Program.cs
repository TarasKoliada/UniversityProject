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
            /*using (var context = new OrderingContext())
            {
                Console.WriteLine("Choose site to visit: ");
               
                foreach (var site in context.Site_Info)
                {
                    Console.WriteLine($"{site.Id}) {site.Name}");
                }

                Console.Write("Your choise: ");
                int choise = int.Parse(Console.ReadLine());

                var foundSite = GetSite(choise);
                
            }*/


            /*IRegistrable registrable = new CustomerRegister();
            registrable.Register();*/
            //registrable = new EmployeeRegister();








            CustomerSiteMenu siteMenu = new CustomerSiteMenu();
            siteMenu.ShowMenu();


        }
        public static Site GetSite(int id)
        {
            using (var context = new OrderingContext())
            {
                foreach (var site in context.Site_Info)
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
}
