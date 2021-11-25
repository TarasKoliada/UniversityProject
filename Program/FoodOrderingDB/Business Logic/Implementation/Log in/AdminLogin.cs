using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Repositories;
using System;
using System.Linq;

namespace FoodOrderingDB.Business_Logic.Implementation
{
    class AdminLogin : ILogin<Administrator>
    {
        private Administrator _administrator;
        private readonly UnitOfWork _unitOfWork;
        public AdminLogin()
        {
            _unitOfWork = new UnitOfWork();
        }
        public Administrator Login()
        {
            bool logged = false;
            var attemptsToLog = 0;
            Console.Write("Enter your Login: ");
            var login = Console.ReadLine();
            _administrator = _unitOfWork.Admins.GetAll().FirstOrDefault(a => a.Login == login);
            if (_administrator == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Administrator with such Login is not registered");
                Console.ResetColor();
                Login();
                return null;
            }

            do
            {
                Console.Write("Enter your Password: ");
                var password = Console.ReadLine();
                if (_administrator.Password != password)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Password");
                    Console.ResetColor();
                    ++attemptsToLog;
                    if (attemptsToLog == 4)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You may have entered an foreign Email or Username, try once more: \n");
                        Console.ResetColor();
                        Login();
                    }
                }

                Console.Clear();
                Console.Write($"\nSuccess! Welcome ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{_administrator.Name} {_administrator.MiddleName}!\n");
                Console.ResetColor();
                logged = true;

            } while (logged != true);

            return _administrator;

        }
    }
}