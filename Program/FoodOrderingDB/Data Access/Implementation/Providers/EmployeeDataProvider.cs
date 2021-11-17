using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation.Providers
{
    class EmployeeDataProvider : IDataProvider<Employee>
    {
        OrderingContext context = new OrderingContext();
        Employee _employee;
        public EmployeeDataProvider(Employee employee)
        {
            _employee = employee;
        }
        public DbSet<Employee> GetContext()
        {
            return context.Employee;
        }

        public Employee GetEntity()
        {
            return _employee;
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
