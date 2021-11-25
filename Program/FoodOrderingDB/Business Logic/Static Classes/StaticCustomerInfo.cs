using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using FoodOrderingDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    static class StaticCustomerInfo
    {
        public static readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public static void GetFullInfo(int id)
        {
            var foundCustomer = _unitOfWork.Customers.Get(id);

            Console.WriteLine($"\n  Id: {foundCustomer.Id}");
            Console.WriteLine($"  Name: {foundCustomer.FirstName}");
            Console.WriteLine($"  Surname: {foundCustomer.Surname}");
            Console.WriteLine($"  Middle name: {foundCustomer.MiddleName}");
            Console.WriteLine($"  Email: {foundCustomer.Email}");
            Console.WriteLine($"  Phone number: {foundCustomer.PhoneNumber}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Username: {foundCustomer.Username}");
            Console.WriteLine($"  Password: {foundCustomer.Password}");
            Console.ResetColor();
        }
        public static void ChangePassword(int id)
        {
            var foundCustomer = _unitOfWork.Customers.GetAll().FirstOrDefault(c => c.Id == id);
            if (foundCustomer == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe customer with such id was not found");
                Console.ResetColor();
                return;
            }
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();


            if (foundCustomer.Password != oldPass)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong password");
                Console.ResetColor();
                return;
            }

            Console.Write("\nEnter your new password: ");
            var newPass = Console.ReadLine();

            foundCustomer.Password = newPass;
            _unitOfWork.Customers.SaveData();

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Password Changed!\n");
            Console.ResetColor();

            return;
        }
    }
}
