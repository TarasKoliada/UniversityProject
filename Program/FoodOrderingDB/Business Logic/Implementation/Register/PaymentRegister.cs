using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Implementation.Providers;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.payment
{
    class PaymentRegister
    {
        Employee _employee;
        public PaymentRegister(Employee employee)
        {
            _employee = employee;
        }
        public void ShowOrdersToProcess()
        {
            var provider = new OrderDataProvider(null);
            foreach (var item in provider.GetContext())
            {
                if (item.EmployeeId == _employee.Id && item.Status == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Orders to process: ");
                    Console.ResetColor();
                    foreach (var order in _employee.Order)
                    {
                        if (order.Status == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Order Id: {order.Id}");
                            Console.ResetColor();
                            Console.WriteLine($"  Customer Id: {order.CustomerId}");
                            Console.WriteLine($"  Created: {order.CreatedDate}");
                            foreach (var details in order.OrderDetails)
                            {
                                Console.WriteLine($"     Dish Id: {details.DishId} | Name: {details.Dish.Name} | Number of service: {details.NumberOfService}");
                                if (details.Note != null)
                                {
                                    Console.WriteLine($"     Note: {details.Note}");
                                }
                                Console.WriteLine($"     Total dish price: {details.TotalDishPrice}$");
                            }
                            Console.Write($"  Order Price: ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{order.TotalPrice}$\n");
                            Console.ResetColor();
                        }
                        
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have no orders to process\n");
                    Console.ResetColor();
                    return;
                }

                Console.WriteLine();
            }

        }

        public void ShowProcessedOrders()
        {
            var provider = new PaymentDataProvider(null);
            if (provider.GetContext().Count() > 0)
            {
                foreach (var payment in _employee.Payment)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" Payment Id: {payment.Id}");
                    Console.ResetColor();
                    Console.WriteLine($"   Created: {payment.CreatedDate}");
                    Console.WriteLine($"   Order Id: {payment.Order.Id}");
                    Console.WriteLine($"   Paid By | Adress - {payment.PaidBy}");
                    Console.Write("   Price: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{payment.Price}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have no processed payments\n");
                Console.ResetColor();
                return;
            }
        }


        public void ProcessOrder()
        {
            var payment = new Payment();
            var provider = new OrderDataProvider(null);
            
            Console.Write("Enter order id You want to Process: ");
            var parsed = int.TryParse(Console.ReadLine(), out int orderId);
            if (parsed)
            {
                var foundOrder = _employee.Order.FirstOrDefault(order => order.Id == orderId);
                if (foundOrder != null)
                {
                    foreach (var item in provider.GetContext())
                    {
                        if (item.Status != true && item.Id == foundOrder.Id)
                        {
                            ChangeOrderStatus(foundOrder);
                            payment.EmployeeId = _employee.Id;
                            payment.OrderId = foundOrder.Id;
                            payment.Price = foundOrder.TotalPrice;
                            payment.PaidBy = foundOrder.Customer.FirstName + " " + foundOrder.Customer.Surname + " | " + foundOrder.Customer.Adress;
                            payment.CreatedDate = DateTime.Now;
                            SetInfoToDb(payment);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Payment processed successfully\n");
                            Console.ResetColor();
                        }
                        else if(item.Status == true && item.Id == foundOrder.Id)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("This order has already been processed\n");
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("It's not your order\n");
                            Console.ResetColor();
                            return;
                        }
                    }
                    
                    
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is no such order in your list\n");
                    Console.ResetColor();
                    ProcessOrder();
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input format\n");
                Console.ResetColor();
                ProcessOrder();
            }
        }
        private void ChangeOrderStatus(Order order)
        {
            IDataProvider<Order> provider = new OrderDataProvider(order);

            foreach (var item in provider.GetContext())
            {
                item.Status = true;
            }
            provider.SaveData();
        }
        private void SetInfoToDb(Payment payment)
        {
            IDataProvider<Payment> provider = new PaymentDataProvider(payment);
            IDataProcessor<Payment> dataSaver = new DbDataProcessor<Payment>();
            dataSaver.ProcessData(provider);
        }
    }
}
