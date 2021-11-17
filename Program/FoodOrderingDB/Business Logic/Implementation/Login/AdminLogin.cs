using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Implementation
{
    class AdminLogin : ILogger<Administrator>
    {
        Administrator administrator;
        public Administrator Login()
        {
            bool logged = false;
            var provider = GetProvider();
            var attemptsToLog = 0;
            Console.Write("\nEnter your Login: ");
            var login = Console.ReadLine();
            administrator = provider.GetContext().FirstOrDefault(admin => admin.Login == login);
            if (administrator != null)
            {
                do
                {
                    Console.Write("Enter your Password: ");
                    var password = Console.ReadLine();
                    if (administrator.Password == password)
                    {
                        Console.WriteLine($"\nSuccess! Welcome {administrator.Name} {administrator.MiddleName}!");
                        logged = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password");
                        ++attemptsToLog;
                        if (attemptsToLog == 4)
                        {
                            Console.WriteLine("You may have entered an foreign Email or Username, try once more: \n");
                            Login();
                        }
                    }
                } while (logged != true);

                return administrator;
            }
            else
            {
                Console.WriteLine("The Administrator with such Login is not registered");
                Login();
                return null;
            }
        }
        private IDataProvider<Administrator> GetProvider()
        {
            return new AdminDataProvider();
        }
    }
}