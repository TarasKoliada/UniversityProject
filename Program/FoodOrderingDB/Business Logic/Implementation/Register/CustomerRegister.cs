using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Repositories;
using System;


namespace FoodOrderingDB
{
    class CustomerRegister : IRegistrable<Customer>
    {
        private readonly UnitOfWork _unitOfWork;
        public CustomerRegister()
        {
            _unitOfWork = new UnitOfWork();
        }
        public Customer Register()
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
            return customer;
        }
        public void SetInfoToDb(Customer entity)
        {
            _unitOfWork.Customers.Add(entity);
        }
    }
}
