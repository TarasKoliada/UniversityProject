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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"'{site.Name}'");
            Console.ResetColor();
            

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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   \nPreferences have been established");
                Console.ResetColor();
            }
            else
            {
                employee.Preferences = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   \nPreferences have not been established");
                Console.ResetColor();
            }
            
            employee.Login = Login.Generate(employee.FirstName, employee.Surname);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\nGenerated Login: ");
            Console.ResetColor();
            Console.WriteLine($"{employee.Login}");

            employee.Password = Password.Generate();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Generated Password: ");
            Console.ResetColor();
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
