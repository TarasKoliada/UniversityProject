using FoodOrderingDB.Business_Logic.Static_Classes;
using FoodOrderingDB.Repositories;
using System;
using System.Linq;

namespace FoodOrderingDB.Business_Logic.payment
{
    class PaymentRegister
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Employee _employee;
        public PaymentRegister(Employee employee)
        {
            _employee = employee;
            _unitOfWork = new UnitOfWork();
        }

        public void ProcessOrder()
        {
            var payment = new Payment();
            
            Console.Write("Enter order id You want to Process: ");

            var parsed = int.TryParse(Console.ReadLine(), out int orderId);
            if (!parsed)
            {
                WriteMessage.Write("Wrong input format\n", ConsoleColor.Red);
                ProcessOrder();
            }

            var foundOrder = _employee.Order.FirstOrDefault(o => o.Id == orderId);

            if (foundOrder == null)
            {
                WriteMessage.Write("There is no such order in your list\n", ConsoleColor.Red);
                ProcessOrder();
            }

            if (foundOrder.Status != true)
            {
                _unitOfWork.Orders.ChangeStatus(true, foundOrder.Id);
                payment.EmployeeId = _employee.Id;
                payment.OrderId = foundOrder.Id;
                payment.Price = foundOrder.TotalPrice;
                payment.PaidBy = foundOrder.Customer.FirstName + " " + foundOrder.Customer.Surname + " | " + foundOrder.Customer.Adress;
                payment.CreatedDate = DateTime.Now;

                SetInfoToDb(payment);

                WriteMessage.Write("Payment processed successfully\n", ConsoleColor.Green);
                return;
            }
            else if (foundOrder.Status == true)
            {
                WriteMessage.Write("This order has already been processed\n", ConsoleColor.Red);
                return;
            }
        }
        private void SetInfoToDb(Payment payment)
        {
            _unitOfWork.Payments.Add(payment);
        }
    }
}
