using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
using FoodOrderingDB.Repositories;
using System;
using System.Linq;

namespace FoodOrderingDB
{
    class CustomerLogin : ILogin<Customer>
    {
        Customer _customer;
        private readonly UnitOfWork _unitOfWork;
        public CustomerLogin()
        {
            _unitOfWork = new UnitOfWork();
        }
        public Customer Login()
        {
            bool logged = false;
            int attemptsToLog = 0;
            Console.Write("\nEnter Email or Username: ");
            var loginValue = Console.ReadLine();

            _customer = _unitOfWork.Customers.GetAll().FirstOrDefault(customer => customer.Email == loginValue || customer.Username == loginValue);

            if (_customer == null)
            {
                WriteMessage.Write("The User with such Username and Email is not registered", ConsoleColor.Red, false);
                Login();
                return null;
            }

            do
            {
                Console.Write("Enter your Password: ");
                var pass = Console.ReadLine();
                if (pass != _customer.Password)
                {
                    ++attemptsToLog;
                    WriteMessage.Write("\nWrong Password\n", ConsoleColor.Red, false);
                    if (attemptsToLog == 4)
                    {
                        Console.WriteLine("You may have entered an foreign Email or Username, try once more: \n");
                        Login();
                    }
                }

                logged = true;
            } while (logged != true);

            return _customer;
        }
    }
}