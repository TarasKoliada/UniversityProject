using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Repositories;
using System;
using System.Linq;

namespace FoodOrderingDB.Business_Logic.Implementation.Log_in
{
    class EmployeeLogin : ILogin<Employee>
    {
        private Employee _employee;
        private readonly UnitOfWork _unitOfWork;
        public EmployeeLogin()
        {
            _unitOfWork = new UnitOfWork();
        }
        public Employee Login()
        {
            bool logged = false;
            var attemptsToLog = 0;
            Console.Write("Enter your Login: ");
            var login = Console.ReadLine();
            _employee = _unitOfWork.Employees.GetAll().FirstOrDefault(admin => admin.Login == login);

            if (_employee == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Employee with such Login is not registered");
                Console.ResetColor();
                Login();
                return null;
            }

            do
            {
                Console.Write("Enter your Password: ");
                var password = Console.ReadLine();
                if (_employee.Password != password)
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
                Console.WriteLine($"{_employee.FirstName} {_employee.MiddleName}!\n");
                Console.ResetColor();
                logged = true;
            } while (logged != true);

            return _employee;
        }
    }
}
