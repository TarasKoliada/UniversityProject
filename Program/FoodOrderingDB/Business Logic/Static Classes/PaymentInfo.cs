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
            WriteMessage.Write("Orders to process: ", ConsoleColor.Green, false);
            foreach (var order in _unitOfWork.Orders.GetAll())
            {
                if (order.Status == false && order.EmployeeId == _employee.Id)
                {
                    WriteMessage.Write($"Order Id: {order.Id}", ConsoleColor.Yellow, false);
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
                    WriteMessage.Write($"{order.TotalPrice}$\n", ConsoleColor.Yellow, false);
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
                    WriteMessage.Write($"\n Payment Id: {payment.Id}", ConsoleColor.Yellow, false);
                    Console.WriteLine($"   Created: {payment.CreatedDate}");
                    Console.WriteLine($"   Order Id: {payment.Order.Id}");
                    Console.WriteLine($"   Paid By | Adress - {payment.PaidBy}");
                    WriteMessage.Write($"   Price: {payment.Price}$", ConsoleColor.Green, false);
                }
            }
        }
    }
}
