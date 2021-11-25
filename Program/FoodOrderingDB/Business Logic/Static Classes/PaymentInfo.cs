using FoodOrderingDB.Repositories;
using System;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class PaymentInfo
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Employee _employee;
        public PaymentInfo(Employee employee)
        {
            _unitOfWork = new UnitOfWork();
            _employee = employee;
        }
        public void ShowOrdersToProcess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Orders to process: ");
            Console.ResetColor();
            foreach (var order in _unitOfWork.Orders.GetAll())
            {
                if (order.Status == false && order.EmployeeId == _employee.Id)
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
            }
        }

        public void ShowProcessedOrders()
        {
            var payments = _unitOfWork.Payments.GetAll();

            foreach (var payment in payments)
            {
                if (payment.EmployeeId == _employee.Id)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n Payment Id: {payment.Id}");
                    Console.ResetColor();
                    Console.WriteLine($"   Created: {payment.CreatedDate}");
                    Console.WriteLine($"   Order Id: {payment.Order.Id}");
                    Console.WriteLine($"   Paid By | Adress - {payment.PaidBy}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"   Price: {payment.Price}$");
                    Console.ResetColor();
                }
            }
        }
    }
}
