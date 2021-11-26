using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using FoodOrderingDB.Repositories;

namespace FoodOrderingDB.Business_Logic.Implementation.Register
{
    class EmployeeRegister : IRegistrable<Employee>
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly int _siteId;
        public EmployeeRegister(int siteId)
        {
            _unitOfWork = new UnitOfWork();
            _siteId = siteId;
        }
        public Employee Register()
        {
            Employee employee = new Employee();

            employee.Siteid = _siteId;

            var site = _unitOfWork.Sites.Get(employee.Siteid);

            Console.Write($"Adding an Employee to site ");
            WriteMessage.Write($"'{site.Name}'", ConsoleColor.Red, false);
            

            Console.Write("\nEnter Employee name: ");
            employee.FirstName = Console.ReadLine();

            Console.Write("Enter Employee surname: ");
            employee.Surname = Console.ReadLine();

            Console.Write("Enter Employee middle name: ");
            employee.MiddleName = Console.ReadLine();

            Console.Write("Enter Employee email: ");
            employee.Email = Console.ReadLine();

            Console.Write("Enter Employee contact: ");
            employee.Contact = Console.ReadLine();

            Console.Write("\nAdd Employee preferences? y/n: ");
            var choise = Console.ReadLine();
            if (choise == "Y" || choise == "y")
            {
                employee.Preferences = true;
                WriteMessage.Write("   \nPreferences have been established", ConsoleColor.Yellow, false);
            }
            else
            {
                employee.Preferences = false;
                WriteMessage.Write("   \nPreferences have not been established", ConsoleColor.Yellow, false);
            }
            
            employee.Login = Login.Generate(employee.FirstName, employee.Surname);
            WriteMessage.Write($"\nGenerated Login: ", ConsoleColor.Yellow, false);
            Console.WriteLine($"{employee.Login}");

            employee.Password = Password.Generate();
            WriteMessage.Write($"Generated Password: ", ConsoleColor.Yellow, false);
            Console.WriteLine($"{employee.Password}");
            
            SetInfoToDb(employee);

            Console.WriteLine("\nPress any key to turn back: ");
            Console.ReadKey();
            Console.Clear();

            return employee;
        }

        public void SetInfoToDb(Employee entity)
        {
            _unitOfWork.Employees.Add(entity);
        }
    }
}
