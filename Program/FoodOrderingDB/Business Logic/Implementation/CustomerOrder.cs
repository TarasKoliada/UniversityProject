using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Implementation
{
    class CustomerOrder
    {
        Order _order = new Order();
        Site _site;
        public CustomerOrder(Customer customer, Site site)
        {
            _order.CustomerId = customer.Id;
            _order.Status = false;
            _order.CreatedDate = DateTime.Now;
            _site = site;
            //InitDetails();
        }
        public void Create()
        {
            var employee = GetEmployee();//
            _order.Employee = employee;//

            ShowSiteMenu();
            ChooseDishes();
            CalculateOrderPrice();
            SaveData();
        }
        private void ShowSiteMenu()
        {
            Console.WriteLine();
            foreach (var menuTypes in _site.MenuType)
            {
                Console.WriteLine($" {+menuTypes.Id}) {menuTypes.Name}");
                foreach (var dishes in menuTypes.Dish)
                {
                    Console.WriteLine($"   {dishes.Id}. {dishes.Name}, {dishes.Price}$, {dishes.Weight}g.");
                }
            }
        }
        private void ChooseDishes()
        {
            var details = new OrderDetails();
            int exit;

            details.OrderId = _order.Id;
            Console.Write("\nEnter the Dish Id you want to add to cart: ");
            details.DishId = int.Parse(Console.ReadLine());

            Console.Write("How many servings of this Dish you want to order: ");
            details.NumberOfService = int.Parse(Console.ReadLine());

            Console.Write("Add some order notes? y/n : ");
            var setNotes = Console.ReadLine();

            if (setNotes == "y" || setNotes == "Y" || setNotes == "Yes" || setNotes == "YES")
            {
                Console.Write("Write Notes you want to add: ");
                details.Note = Console.ReadLine();
            }

            CalculateDishPrice(details);
            SaveOrderDetails(details);

            do
            {
                Console.WriteLine("\nAdd one more Dish? ");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) Turn Back");
                Console.Write("Your choise: ");
                bool parsed = int.TryParse(Console.ReadLine(), out exit);
                if (parsed && exit == 1)
                {
                    ChooseDishes();
                }
                else if (parsed && exit == 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nWrong input. Try once more");
                }
            } while (exit == 2);
           
        }
        /* private void InitDetails()
         {
             foreach (var details in _order.OrderDetails)
             {
                 details.Order = _order;


                 //details.OrderId = _order.Id;
             }
         }*/
        private void CalculateDishPrice(OrderDetails details)
        {
            details.TotalDishPrice = details.NumberOfService * GetDishPrice(details.DishId);
        }
        private double GetDishPrice(int dishId)
        {
            foreach (var menuTypes in _site.MenuType)
            {
                foreach (var dishes in menuTypes.Dish)
                {
                    if (dishes.Id == dishId)
                    {
                        return dishes.Price;
                    }
                }
            }
            return -1;
        }
        private void CalculateOrderPrice()
        {
            foreach (var item in _order.OrderDetails)
            {
                _order.TotalPrice += item.TotalDishPrice;
            }
        }
        private void SaveOrderDetails(OrderDetails details)
        {
            _order.OrderDetails.Add(details);
        }
        private void SaveData()
        {
            IDataProvider<Order> provider = new OrderDataProvider(_order);
            IDataProcessor<Order> dataSaver = new DbDataProcessor<Order>();
            dataSaver.ProcessData(provider);
        }

        private Employee GetEmployee()
        {
            Employee employee = new Employee
            {
                FirstName = "test",
                Siteid = 1,
                Surname = "test",
                Contact = "text",
                Email = "test",
                Login = "test",
                MiddleName = "test",
                Password = "test",
                Preferences = true
            };
            return employee;
        }
    }
}
