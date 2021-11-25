using FoodOrderingDB.Business_Logic.Implementation.Ordering;
using FoodOrderingDB.Business_Logic.Static_Classes;
using FoodOrderingDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Business_Logic.Implementation
{
    class CustomerOrder
    {
        private readonly UnitOfWork _unitOfWork;
        private Order _order = new Order();
        private Customer _customer = new Customer();
        private Site _site;
        public CustomerOrder(Customer customer, Site site)
        {
            _unitOfWork = new UnitOfWork();
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
            foreach (var order in _unitOfWork.Orders.GetAll().Where(o => o.CustomerId == _customer.Id))
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
            bool parsed;
            StaticSiteInfo.ShowSiteMenu(_site.Id);

            details.OrderId = _order.Id;

            Console.Write("\n\nEnter the Dish Id you want to add to cart: ");
            var dishes = GetSiteDishes();
            parsed = int.TryParse(Console.ReadLine(), out int dishId);
            if (!parsed)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong Input format\n");
                Console.ResetColor();
                SetDetails();
            }

            try
            {
                details.DishId = dishes[dishId - 1].Id;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such Dish in the menu");
                Console.ResetColor();
                SetDetails();
            }


            do
            {
                Console.Write("How many servings of this Dish you want to order: ");
                parsed = int.TryParse(Console.ReadLine(), out int service);
                if (!parsed)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong Input format\n");
                    Console.ResetColor();
                }
                else
                {
                    details.NumberOfService = service;
                }
            } while (!parsed);


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
            _unitOfWork.Orders.Add(_order);
        }
    }
}