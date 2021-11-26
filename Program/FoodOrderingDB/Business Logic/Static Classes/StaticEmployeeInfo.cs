using FoodOrderingDB.Repositories;
using System;
using System.Linq;


namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class StaticEmployeeInfo
    {
        private static UnitOfWork _unitOfWork;
        public static void GetSiteEmployees(int siteId)
        {
            _unitOfWork = new UnitOfWork();
            foreach (var employee in _unitOfWork.Employees.GetAll().Where(s => s.Siteid == siteId))
            {
                Console.WriteLine();
                WriteMessage.Write($"ID: {employee.Id} ", ConsoleColor.Red, false);
                Console.WriteLine($"{employee.FirstName} {employee.Surname} {employee.MiddleName} | {employee.Contact} | {employee.Email}");

                if (_unitOfWork.Orders.GetAll().Where(o => o.EmployeeId == employee.Id).ToList().Count > 0)
                {
                    WriteMessage.Write("  Orders: ", ConsoleColor.Green, false);
                    foreach (var order in employee.Order)
                    {
                        Console.WriteLine($"    Order Id: {order.Id} | Status {order.Status} | Created: {order.CreatedDate} | Price: {order.TotalPrice}");
                    }
                }

                WriteMessage.Write($"   Login: ", ConsoleColor.Yellow, false);
                Console.WriteLine($"{employee.Login}");

                WriteMessage.Write($"   Password: ", ConsoleColor.Yellow, false);
                Console.WriteLine($"{employee.Password}");
            }
            Console.WriteLine("\n\n");
        }
        public static void ChangePassword(int id)
        {
            _unitOfWork = new UnitOfWork();
            var foundEmployee = _unitOfWork.Employees.GetAll().FirstOrDefault(e => e.Id == id);
            if (foundEmployee == null)
            {
                WriteMessage.Write("\nThe employee with such id was not found", ConsoleColor.Red, false);
                return;
            }
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();

            
            if (foundEmployee.Password != oldPass)
            {
                WriteMessage.Write("\nWrong password", ConsoleColor.Red);;
                return;
            }

            Console.Write("\nEnter your new password: ");
            var newPass = Console.ReadLine();

            foundEmployee.Password = newPass;
            _unitOfWork.Employees.SaveData();

            WriteMessage.Write("Password Changed!\n", ConsoleColor.Yellow);
            return;

        }
        public static void GetCurrentEmployeeInfo(int id)
        {
            _unitOfWork = new UnitOfWork();
            var employee = _unitOfWork.Employees.Get(id);

            var paymentsCount = _unitOfWork.Payments.GetAll().Count(p => p.EmployeeId == employee.Id);

            Console.WriteLine($"\n  Id: {employee.Id}");
            Console.WriteLine($"  Name: {employee.FirstName}");
            Console.WriteLine($"  Surname: {employee.Surname}");
            Console.WriteLine($"  Middle name: {employee.MiddleName}");
            Console.WriteLine($"  Email: {employee.Email}");
            Console.WriteLine($"  Phone number: {employee.Contact}");
            Console.WriteLine($"  Preferences: {employee.Preferences}");
            Console.WriteLine($"  Orders count: {employee.Order.Count}");
            Console.WriteLine($"  Processed orders Count: {paymentsCount}");
            Console.Write($"  Username: ");
            WriteMessage.Write($"{employee.Login}", ConsoleColor.Yellow, false);
            Console.Write($"  Password: ");
            WriteMessage.Write($"{employee.Password}\n", ConsoleColor.Yellow, false);
            
        }
    }
}
