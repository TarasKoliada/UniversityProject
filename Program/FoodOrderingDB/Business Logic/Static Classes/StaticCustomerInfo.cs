using FoodOrderingDB.Repositories;
using System;
using System.Linq;

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
            WriteMessage.Write($"  Username: {foundCustomer.Username}\n  Password: {foundCustomer.Password}", ConsoleColor.Yellow, false);
        }
        public static void ChangePassword(int id)
        {
            var foundCustomer = _unitOfWork.Customers.GetAll().FirstOrDefault(c => c.Id == id);
            if (foundCustomer == null)
            {
                WriteMessage.Write("\nThe customer with such id was not found", ConsoleColor.Red, false);
                return;
            }
            Console.Write("\nEnter your previous password: ");
            var oldPass = Console.ReadLine();


            if (foundCustomer.Password != oldPass)
            {
                WriteMessage.Write("\nWrong password", ConsoleColor.Red);
                return;
            }

            Console.Write("\nEnter your new password: ");
            var newPass = Console.ReadLine();

            foundCustomer.Password = newPass;
            _unitOfWork.Customers.SaveData();

            WriteMessage.Write("Password Changed!\n", ConsoleColor.Yellow);

            return;
        }
    }
}
