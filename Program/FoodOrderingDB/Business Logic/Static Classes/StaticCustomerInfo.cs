using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
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
            var provider = new CustomerDataProvider(customer);
            var findCustomer = provider.GetContext().Where(c => c.Id == customer.Id).FirstOrDefault();

            Console.WriteLine($"\n  Id: {findCustomer.Id}");
            Console.WriteLine($"  Name: {findCustomer.FirstName}");
            Console.WriteLine($"  Surname: {findCustomer.Surname}");
            Console.WriteLine($"  Middle name: {findCustomer.MiddleName}");
            Console.WriteLine($"  Email: {findCustomer.Email}");
            Console.WriteLine($"  Phone number: {findCustomer.PhoneNumber}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Username: {findCustomer.Username}");
            Console.WriteLine($"  Password: {findCustomer.Password}");
            Console.ResetColor();
        }
        public static void ChangePassword()
        {
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();

            var context = new OrderingContext();
            var customer = context.Customer.Where(c => c.Password == oldPass).FirstOrDefault();
            if (customer != null)
            {
                Console.Write("\nEnter your new password: ");
                var newPass = Console.ReadLine();

                customer.Password = newPass;

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
    }
}
