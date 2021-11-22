using FoodOrderingDB.Data_Access.Implementation.Providers;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class StaticEmployeeInfo
    {
        public static void GetSiteEmployees(Site site)
        {
            IDataProvider<Employee> provider = new EmployeeDataProvider(null);
            foreach (var employee in provider.GetContext())
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"ID: {employee.Id} ");
                Console.ResetColor();
                Console.WriteLine($"{employee.FirstName} {employee.Surname} {employee.MiddleName} | {employee.Contact} | {employee.Email}");
                if (employee.Order.Count > 0)
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
        public static void ChangePassword()
        {
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();

            var context = new OrderingContext();
            var employee = context.Employee.Where(e => e.Password == oldPass).FirstOrDefault();
            if (employee != null)
            {
                Console.Write("\nEnter your new password: ");
                var newPass = Console.ReadLine();

                employee.Password = newPass;

                context.SaveChanges();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong password");
                Console.ResetColor();
                ChangePassword();
            }

            Console.Clear();

        }
        public static void GetCurrentEmployeeInfo(Employee employee)
        {
            var provider = new PaymentDataProvider(null);
            
            Console.WriteLine($"\n  Id: {employee.Id}");
            Console.WriteLine($"  Name: {employee.FirstName}");
            Console.WriteLine($"  Surname: {employee.Surname}");
            Console.WriteLine($"  Middle name: {employee.MiddleName}");
            Console.WriteLine($"  Email: {employee.Email}");
            Console.WriteLine($"  Phone number: {employee.Contact}");
            Console.WriteLine($"  Preferences: {employee.Preferences}");
            Console.WriteLine($"  Orders count: {employee.Order.Count}");
            foreach (var payment in provider.GetContext())
            {
                if (payment.EmployeeId == employee.Id)
                {
                    Console.WriteLine($"  Processed orders Count: {payment.Employee.Payment.Count}");
                }
            }
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
