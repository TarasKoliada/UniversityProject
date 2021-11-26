using System;
using System.Linq;
using FoodOrderingDB.Business_Logic.Implementation.Ordering;
using FoodOrderingDB.Business_Logic.Static_Classes;
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
                WriteMessage.Write("Wrong input format. Try more!", ConsoleColor.Red);
                Remove();
            }

            var deleteEmployee = _unitOfWork.Employees.GetAll().FirstOrDefault(e => e.Id == removeId);

            var newEmployeeId = EmployeeSetter.GetChangedEmployeeId(deleteEmployee);
            if (newEmployeeId == -1)
            {
                WriteMessage.Write("You can not delete this Employee, because he is the last worker", ConsoleColor.Red);
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

            WriteMessage.Write("\bEmployee removed\n", ConsoleColor.Yellow);
        }
    }
}
