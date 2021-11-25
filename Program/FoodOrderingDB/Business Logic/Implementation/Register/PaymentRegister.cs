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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input format\n");
                Console.ResetColor();
                ProcessOrder();
            }

            var foundOrder = _employee.Order.FirstOrDefault(o => o.Id == orderId);

            if (foundOrder == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such order in your list\n");
                Console.ResetColor();
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

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Payment processed successfully\n");
                Console.ResetColor();
                return;
            }
            else if (foundOrder.Status == true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This order has already been processed\n");
                Console.ResetColor();
                return;
            }
        }
        private void SetInfoToDb(Payment payment)
        {
            _unitOfWork.Payments.Add(payment);
        }
    }
}
