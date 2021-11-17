using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Implementation.Providers;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Implementation.Register
{
    class EmployeeRegister : IRegistrable<Employee>
    {
        int _siteId;
        public EmployeeRegister(int siteId)
        {
            _siteId = siteId;
        }
        public Employee Register()
        {
            Employee employee = new Employee();

            employee.Siteid = _siteId;

            var site = StaticSiteInfo.GetSiteById(employee.Siteid);

            Console.WriteLine($"Adding an Employee to site '{site.Name}'");
            

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
                Console.WriteLine("   \nPreferences have been established");
            }
            else
            {
                employee.Preferences = false;
                Console.WriteLine("   \nPreferences have not been established");
            }

            employee.Login = Login.Generate(employee.FirstName, employee.Surname);
            Console.WriteLine($"\nGenerated Login: {employee.Login}");

            employee.Password = Password.Generate();
            Console.WriteLine($"Generated Password: {employee.Password}");


            SetInfoToDb(employee);

            return employee;
        }

        public void SetInfoToDb(Employee entity)
        {
            IDataProvider<Employee> provider = new EmployeeDataProvider(entity);
            IDataProcessor<Employee> dataSaver = new DbDataProcessor<Employee>();
            dataSaver.ProcessData(provider);
        }
    }
}
