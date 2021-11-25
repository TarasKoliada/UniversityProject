using System;
using System.Diagnostics;

namespace FoodOrderingDB.User_Interface
{
    class SiteNavigation
    {
        public void ShowMenu()
        {
            int choise;

            Console.WriteLine("1) Login as User");
            Console.WriteLine("2) Login as Employee");
            Console.WriteLine("3) Login as Administrator");
            Console.WriteLine("4) Finish Program");
            Console.Write("Your choise: ");
            choise = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (choise)
            {
                case 1:
                    var customerMenu = new CustomerSiteMenu();
                    customerMenu.ShowMenu();
                    ShowMenu();
                    break;
                case 2:
                    var employeeMenu = new EmployeeSiteMenu();
                    employeeMenu.ShowMenu();
                    ShowMenu();
                    break;
                case 3:
                    var adminMenu = new AdminSiteMenu();
                    adminMenu.ShowMenu();
                    ShowMenu();
                    break;
                case 4:
                    CloseProgram();
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Select options 1 to 4\n");
                    Console.ResetColor();
                    ShowMenu();
                    break;
            }
        }
        private void CloseProgram()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
