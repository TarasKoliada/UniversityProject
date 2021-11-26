using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
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
                WriteMessage.Write("The Employee with such Login is not registered", ConsoleColor.Red, false);
                Login();
                return null;
            }

            do
            {
                Console.Write("Enter your Password: ");
                var password = Console.ReadLine();
                if (_employee.Password != password)
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
                WriteMessage.Write($"{_employee.FirstName} {_employee.MiddleName}!\n", ConsoleColor.Blue, false);
                logged = true;
            } while (logged != true);

            return _employee;
        }
    }
}
