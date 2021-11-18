using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    static class StaticCustomerInfo
    {
        public static void GetFullInfo(Customer customer)
        {
            Console.WriteLine($"\n  Id: {customer.Id}");
            Console.WriteLine($"  Name: {customer.FirstName}");
            Console.WriteLine($"  Surname: {customer.Surname}");
            Console.WriteLine($"  Middle name: {customer.MiddleName}");
            Console.WriteLine($"  Email: {customer.Email}");
            Console.WriteLine($"  Phone number: {customer.PhoneNumber}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Username: {customer.Username}");
            Console.WriteLine($"  Password: {customer.Password}");
            Console.ResetColor();
        }
    }
}
