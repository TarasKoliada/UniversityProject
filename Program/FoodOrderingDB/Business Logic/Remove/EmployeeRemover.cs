using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Implementation.Providers;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using FoodOrderingDB.Business_Logic.Implementation.Ordering;

namespace FoodOrderingDB.Business_Logic.Remove
{
    class EmployeeRemover
    {
        Administrator _admin;
        public EmployeeRemover(Administrator admin)
        {
             _admin = admin;
        }
        public void Remove()
        {
            var context = new OrderingContext();
            
            Console.WriteLine("Enter Employee Id you want to remove: ");
            var parsed = int.TryParse(Console.ReadLine(), out int removeId);
            if (parsed)
            {
                var deleteEmployee = context.Employee
                    .Where(e => e.Id == removeId)
                    .FirstOrDefault();

                var newEmployeeId = EmployeeSetter.GetChangedEmployeeId(deleteEmployee);
                if (newEmployeeId != -1)
                {
                    foreach (var order in context.Orders)
                    {
                        if (order.EmployeeId == deleteEmployee.Id)
                        {
                            order.EmployeeId = newEmployeeId;
                            foreach (var payment in order.Payment)
                            {
                                payment.EmployeeId = newEmployeeId;
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You can not delete this Employee, because he is the last worker");
                    Console.ResetColor();
                    return;
                }

                context.Employee.Remove(deleteEmployee);
                context.SaveChanges();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input format. Try more!");
                Console.ResetColor();
                Remove();
            }
        }
    }
}
