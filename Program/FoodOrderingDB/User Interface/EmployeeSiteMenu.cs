using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation.Log_in;
using FoodOrderingDB.Business_Logic.payment;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.User_Interface
{
    class EmployeeSiteMenu
    {
        Employee _employee;
        public EmployeeSiteMenu()
        {
            Login();
        }
        public void ShowMenu()
        {
            int choise;
            Console.WriteLine("1) Show my profile info");
            Console.WriteLine("2) Orders to process");
            Console.WriteLine("3) Process order");
            Console.WriteLine("4) Show my processed orders");
            Console.WriteLine("5) Log out");
            Console.Write("Your choise: ");

            var payment = new PaymentRegister(_employee);

            var parsed = int.TryParse(Console.ReadLine(), out choise);
            if (parsed)
            {
                switch (choise)
                {
                    case 1:
                        Console.Clear();
                        StaticEmployeeInfo.GetCurrentEmployeeInfo(_employee);
                        ShowMenu();
                        break;
                    case 2:
                        Console.Clear();
                        payment.ShowOrdersToProcess();
                        ShowMenu();
                        break;
                    case 3:
                        Console.Clear();
                        payment.ProcessOrder();
                        ShowMenu();
                        break;
                    case 4:
                        Console.Clear();
                        payment.ShowProcessedOrders();
                        ShowMenu();
                        break;
                    case 5:
                        Console.Clear();
                        _employee = null;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Input");
                        Console.ResetColor();
                        ShowMenu();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Input Format");
                Console.ResetColor();
                ShowMenu();
            }
        }
        private void Login()
        {
            ILogger<Employee> logger = new EmployeeLogin();
            _employee = logger.Login();
        }
    }
}
