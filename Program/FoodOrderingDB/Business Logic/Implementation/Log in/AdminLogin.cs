using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
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
                WriteMessage.Write("The Administrator with such Login is not registered", ConsoleColor.Red);
                Login();
                return null;
            }

            do
            {
                Console.Write("Enter your Password: ");
                var password = Console.ReadLine();
                if (_administrator.Password != password)
                {
                    WriteMessage.Write("Wrong Password", ConsoleColor.Red);
                    ++attemptsToLog;
                    if (attemptsToLog == 4)
                    {
                        WriteMessage.Write("You may have entered an foreign Email or Username, try once more: \n", ConsoleColor.Red);
                        Login();
                    }
                }

                Console.Clear();
                Console.Write($"\nSuccess! Welcome ");
                WriteMessage.Write($"{_administrator.Name} {_administrator.MiddleName}!\n", ConsoleColor.Blue, false);
                logged = true;

            } while (logged != true);

            return _administrator;
        }
    }
}