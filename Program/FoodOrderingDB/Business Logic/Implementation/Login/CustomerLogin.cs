using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB
{
    class CustomerLogin : ILogger<Customer>
    {
        Customer _customer;
        public Customer Login()
        {
            bool logged = false;
            var provider = GetProvider();
            int attemptsToLog = 0;
            Console.Write("\nEnter Email or Username: ");
            var loginValue = Console.ReadLine();

            _customer = provider.GetContext().FirstOrDefault(customer => customer.Email == loginValue || customer.Username == loginValue);

            if (_customer != null)
            {
                do
                {
                    Console.Write("Enter your Password: ");
                    var pass = Console.ReadLine();
                    if (pass == _customer.Password)
                    {
                        logged = true;
                    }
                    else
                    {
                        ++attemptsToLog;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nWrong Password\n");
                        Console.ResetColor();
                        if (attemptsToLog == 4)
                        {
                            Console.WriteLine("You may have entered an foreign Email or Username, try once more: \n");
                            Login();
                        }
                    }
                } while (logged != true);

                return _customer;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The User with such Username and Email is not registered");
                Console.ResetColor();
                Login();
                return null;
            }
        }
        private IDataProvider<Customer> GetProvider()
        {
            return new CustomerDataProvider(null);
        }
    }
}