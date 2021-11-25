using System;
using System.Linq;
using FoodOrderingDB.Business_Logic.Implementation.Ordering;
using FoodOrderingDB.Repositories;

namespace FoodOrderingDB.Business_Logic.Remove
{
    class EmployeeRemover
    {
        private readonly Administrator _admin;
        private readonly UnitOfWork _unitOfWork;
        public EmployeeRemover(Administrator admin)
        {
            _unitOfWork = new UnitOfWork();
            _admin = admin;
        }
        public void Remove()
        {
            Console.WriteLine("Enter Employee Id you want to remove: ");
            var parsed = int.TryParse(Console.ReadLine(), out int removeId);
            if (!parsed)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input format. Try more!");
                Console.ResetColor();
                Remove();
            }

            var deleteEmployee = _unitOfWork.Employees.GetAll().FirstOrDefault(e => e.Id == removeId);

            var newEmployeeId = EmployeeSetter.GetChangedEmployeeId(deleteEmployee);
            if (newEmployeeId == -1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You can not delete this Employee, because he is the last worker");
                Console.ResetColor();
                return;
            }
            foreach (var order in _unitOfWork.Orders.GetAll().Where(o => o.EmployeeId == deleteEmployee.Id))
            {
                order.EmployeeId = newEmployeeId;
                foreach (var payment in order.Payment)
                {
                    payment.EmployeeId = newEmployeeId;
                }
            }

            _unitOfWork.Employees.Delete(deleteEmployee.Id);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\bEmployee removed\n");
            Console.ResetColor();
        }
    }
}
