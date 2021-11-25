using FoodOrderingDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Business_Logic.Implementation.Ordering
{
    public static class EmployeeSetter
    {
        private static readonly UnitOfWork _unitOfWork = new UnitOfWork();
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
                foreach (var item in siteWorkers)
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

            if (modifiedWorkers.Count >= 2)
            {
                var minimalOrdersCount = siteWorkers.Min(a => a.Order.Count);
                var employeeIds = GetWantedEmployeesId(minimalOrdersCount, modifiedWorkers);

                return employeeIds[new Random().Next(0, employeeIds.Count)];
            }
            else if (modifiedWorkers.Count == 1)
            {
                return modifiedWorkers.First().Id;
            }
            return -1;
        }
        private static List<Employee> GetSiteEmployee(int id)
        {
            return _unitOfWork.Employees.GetAll().Where(e => e.Siteid == id).ToList();
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
