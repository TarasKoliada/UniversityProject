using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Implementation.Log_in;
using FoodOrderingDB.Business_Logic.payment;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;

namespace FoodOrderingDB.User_Interface
{
    class EmployeeSiteMenu
    {
        private Employee _employee;
        public EmployeeSiteMenu()
        {
            Login();
        }
        public void ShowMenu()
        {
            Console.WriteLine("1) Show my profile info");
            Console.WriteLine("2) Orders to process");
            Console.WriteLine("3) Process order");
            Console.WriteLine("4) Show my processed orders");
            Console.WriteLine("5) Log out");
            Console.Write("Your choise: ");

            var payment = new PaymentRegister(_employee);

            var info = new PaymentInfo(_employee);

            var parsed = int.TryParse(Console.ReadLine(), out int choise);
            if (!parsed)
            {
                WriteMessage.Write("Wrong Input Format", ConsoleColor.Red);
                ShowMenu();
            }

            switch (choise)
            {

                case 1:
                    Console.Clear();
                    StaticEmployeeInfo.GetCurrentEmployeeInfo(_employee.Id);
                    ChangePassMenu();
                    ShowMenu();
                    break;
                case 2:
                    Console.Clear();
                    info.ShowOrdersToProcess();
                    ShowMenu();
                    break;
                case 3:
                    Console.Clear();
                    payment.ProcessOrder();
                    ShowMenu();
                    break;
                case 4:
                    Console.Clear();
                    info.ShowProcessedOrders();
                    ShowMenu();
                    break;
                case 5:
                    Console.Clear();
                    _employee = null;
                    break;
                default:
                    WriteMessage.Write("Wrong Input", ConsoleColor.Red);
                    ShowMenu();
                    break;
            }
        }
        private void ChangePassMenu()
        {
            Console.WriteLine("\n  1) Change password");
            Console.WriteLine("  2) Turn back");
            Console.Write("  Your choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out int choiseProfile);
            if (parsed)
            {
                switch (choiseProfile)
                {
                    case 1:
                        StaticEmployeeInfo.ChangePassword(_employee.Id);
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    default:
                        WriteMessage.Write("Wrong Input", ConsoleColor.Red, false);
                        break;
                }
            }
        }
        private void Login()
        {
            ILogin<Employee> log = new EmployeeLogin();
            _employee = log.Login();
        }
    }
}
