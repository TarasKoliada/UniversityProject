using FoodOrderingDB.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB
{
    class CustomerLogin : ILogger
    {
        public void Login()
        {
            Console.Write("Enter Email or Username: ");
            var loginValue = Console.ReadLine();

            using (var context = new OrderingContext())
            {
                var foundCustomer = context.Customer.FirstOrDefault(logValue => logValue.Email == loginValue || logValue.Username == loginValue);
                if (foundCustomer != null)
                {
                    Console.Write("Enter your Password: ");
                    var pass = Console.ReadLine();
                    if (pass == foundCustomer.Password)
                    {
                        Console.WriteLine($"Success! Welcome {foundCustomer.FirstName}!");
                    }
                    else//передбачити повторне введення паролю
                    {
                        Console.WriteLine("Wrong Password");
                    }
                }
                else//передбачити повторне введення логіну
                {
                    Console.WriteLine("The User with such Username and Email is not registered");
                }
            }

        }
    }
}
