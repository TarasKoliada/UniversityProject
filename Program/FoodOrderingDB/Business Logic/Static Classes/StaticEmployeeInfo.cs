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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"ID: {employee.Id} ");
                Console.ResetColor();
                Console.WriteLine($"{employee.FirstName} {employee.Surname} {employee.MiddleName} | {employee.Contact} | {employee.Email}");

                if (_unitOfWork.Orders.GetAll().Where(o => o.EmployeeId == employee.Id).ToList().Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Orders: ");
                    Console.ResetColor();
                    foreach (var order in employee.Order)
                    {
                        Console.WriteLine($"    Order Id: {order.Id} | Status {order.Status} | Created: {order.CreatedDate} | Price: {order.TotalPrice}");
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"   Login: ");
                Console.ResetColor();
                Console.WriteLine($"{employee.Login}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"   Password: ");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe employee with such id was not found");
                Console.ResetColor();
                return;
            }
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();

            
            if (foundEmployee.Password != oldPass)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong password");
                Console.ResetColor();
                return;
            }

            Console.Write("\nEnter your new password: ");
            var newPass = Console.ReadLine();

            foundEmployee.Password = newPass;
            _unitOfWork.Employees.SaveData();

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Password Changed!\n");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{employee.Login}");
            Console.ResetColor();
            Console.Write($"  Password: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{employee.Password}\n");
            Console.ResetColor();
            
        }
    }
}
