using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Implementation.Providers;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Implementation.Ordering
{
    public static class EmployeeSetter
    {
        public static int GetId(int customerSiteId)
        {
            var siteWorkers = GetSiteEmployee(customerSiteId);

            if (siteWorkers.Count >= 2)
            {
                var minimalOrdersCount = siteWorkers.Min(a => a.Order.Count);
                var employeeIds = GetWantedEmployeesId(minimalOrdersCount, siteWorkers);

                return employeeIds[new Random().Next(0, employeeIds.Count)];
            }
            else
            {
                var context = new OrderingContext();
                foreach (var item in context.Employee)
                {
                    return item.Id;
                }
            }
            return -1;
        }
        public static int GetChangedEmployeeId(Employee deletedEmployee)
        {
            var siteWorkers = GetSiteEmployee(deletedEmployee.Siteid);
            var modifiedWorkers = new List<Employee>();
            foreach (var item in siteWorkers)
            {
                if (item.Id != deletedEmployee.Id)
                {
                    modifiedWorkers.Add(item);
                }
            }

            if (modifiedWorkers.Count > 2)
            {
                var minimalOrdersCount = siteWorkers.Min(a => a.Order.Count);
                var employeeIds = GetWantedEmployeesId(minimalOrdersCount, siteWorkers);

                return employeeIds[new Random().Next(0, employeeIds.Count)];
            }
            else if (modifiedWorkers.Count == 2)
            {
                var context = new OrderingContext();
                foreach (var item in context.Employee)
                {
                    if (item.Id != deletedEmployee.Id)
                    {
                        return item.Id;
                    }
                }
            }
            return -1;
        }
        private static List<Employee> GetSiteEmployee(int id)
        {
            var list = new List<Employee>();
            IDataProvider<Employee> provider = new EmployeeDataProvider(null);
            foreach (var employees in provider.GetContext())
            {
                if (employees.Siteid == id)
                {
                    list.Add(employees);
                }
            }
            return list;
        }
        private static List<int> GetWantedEmployeesId(int minOrdersCount, List<Employee> siteWorkers)
        {
            List<int> employeeIds = new List<int>();
            foreach (var item in siteWorkers)
            {
                if (item.Order.Count == minOrdersCount)
                {
                    employeeIds.Add(item.Id);
                }
            }
            return employeeIds;
        }
    }
}
