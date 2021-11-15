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
            bool logged = false;
            int attemptsToLog = 0;
            Console.Write("\nEnter Email or Username: ");
            var loginValue = Console.ReadLine();

            using (var context = new OrderingContext())
            {
                var foundCustomer = context.Customer.FirstOrDefault(logValue => logValue.Email == loginValue || logValue.Username == loginValue);
                if (foundCustomer != null)
                {
                    do
                    {
                        Console.Write("Enter your Password: ");
                        var pass = Console.ReadLine();
                        if (pass == foundCustomer.Password)
                        {
                            Console.WriteLine($"\nSuccess! Welcome {foundCustomer.FirstName}!");
                            logged = true;
                        }
                        else
                        {
                            ++attemptsToLog;
                            Console.WriteLine("Wrong Password\n");
                            if (attemptsToLog == 4)
                            {
                                Console.WriteLine("You may have entered an foreign Email or Username, try once more: \n");
                                Login();
                            }
                        }
                    } while (logged != true);                }
                else
                {
                    Console.WriteLine("The User with such Username and Email is not registered");
                    Login();
                }
            }

        }
    }
}
