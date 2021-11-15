using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB
{
    class CustomerRegister : IRegistrable
    {

        public void Register()
        {
            Customer customer = new Customer();

            customer.AccountStatus = true;

            Console.Write("\nEnter your Name: ");
            customer.FirstName = Console.ReadLine();
            Console.Write("Enter your Surname: ");
            customer.Surname = Console.ReadLine();
            Console.Write("Enter your Middle name: ");
            customer.MiddleName = Console.ReadLine();

            Console.Write("What is your Email: ");
            customer.Email = Console.ReadLine();
            Console.Write("What is your Phone Number: ");
            customer.PhoneNumber = Console.ReadLine();
            Console.Write("What is your Adress: ");
            customer.Adress = Console.ReadLine();

            Console.WriteLine("\nAnd finally: ");

            Console.Write("\nCreate your Username: ");
            customer.Username = Console.ReadLine();
            Console.Write("Create your Password: ");
            customer.Password = Console.ReadLine();


            SetInfoToDb(customer);
        }

        public void SetInfoToDb<T>(T entity)
        {
            T newObj = (T)(object)entity;
            Customer customer = (Customer)(object)newObj;

            IDataProvider<Customer> provider = new CustomerDataProvider(customer);
            IDataProcessor<Customer> dataSaver = new DbDataProcessor<Customer>();
            dataSaver.ProcessData(provider);
        }
    }
}
