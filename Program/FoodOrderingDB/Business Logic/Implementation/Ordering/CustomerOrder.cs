using FoodOrderingDB.Business_Logic.Implementation.Ordering;
using FoodOrderingDB.Business_Logic.Static_Classes;
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
        Customer _customer = new Customer();
        Site _site;
        public CustomerOrder(Customer customer, Site site)
        {
            _customer = customer;
            _site = site;
        }


        public void Create()
        {
            _order.CustomerId = _customer.Id;
            _order.Status = false;
            _order.CreatedDate = DateTime.Now;
            _order.EmployeeId = EmployeeSetter.GetId(_site.Id);

            
            SetDetails();
            
            CalculateOrderPrice();
            SaveData();
        }


        public void GetOrders()
        {
            IDataProvider<Order> provider = new OrderDataProvider(null);
            foreach (var order in provider.GetContext())
            {
                Console.WriteLine("-----------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Id: {order.Id}");
                Console.ResetColor();
                Console.WriteLine($"Status: {order.Status}");
                Console.WriteLine($"Creation date: {order.CreatedDate}");
                Console.WriteLine($"Responsible employee identifier: {order.EmployeeId}");
                Console.WriteLine("Order details: ");
                foreach (var details in order.OrderDetails)
                {
                    Console.WriteLine($"   Dish id: {details.Dish.Id}");
                    Console.WriteLine($"   Dish name: {details.Dish.Name}");
                    Console.WriteLine($"   Dish price: {details.Dish.Price}");
                    Console.WriteLine($"   Number of dishes: {details.NumberOfService}");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Total price: {order.TotalPrice}$");
                Console.ResetColor();
                Console.WriteLine("-----------------------------------------");
            }


        }

        private void SetDetails()
        {
            var details = new OrderDetails();
            StaticSiteInfo.ShowSiteMenu(_site);

            details.OrderId = _order.Id;

            Console.Write("\n\nEnter the Dish Id you want to add to cart: ");
            int dishId = int.Parse(Console.ReadLine());
            var dishes = GetSiteDishes();
            details.DishId = dishes[dishId - 1].Id;

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

            Console.Write("\nAdd one more Dish? y/n: ");
            var addMore = Console.ReadLine();
            if (addMore == "y" || addMore == "Y" || addMore == "Yes" || addMore == "YES")
            {
                Console.Clear();
                SetDetails();
            }
            Console.Clear();

        }
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
        private List<Dish> GetSiteDishes()
        {
            List<Dish> dishes = new List<Dish>();
            foreach (var menuType in _site.MenuType)
            {
                foreach (var dish in menuType.Dish)
                {
                    dishes.Add(dish);
                }
            }
            return dishes;
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
    }
}