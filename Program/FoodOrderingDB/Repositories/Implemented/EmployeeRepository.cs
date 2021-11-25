using FoodOrderingDB.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Repositories.Implemented
{
    class EmployeeRepository : IRepository<Employee>
    {
        private readonly OrderingContext _orderingContext;
        public EmployeeRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Employee entity)
        {
            if (entity != null)
            {
                _orderingContext.Employee.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var employee = Get(id);
            if (employee != null)
            {
                _orderingContext.Employee.Remove(employee);
                _orderingContext.SaveChanges();
            }
        }
        public void SaveData()
        {
            _orderingContext.SaveChanges();
        }
        public Employee Get(int id)
            => _orderingContext.Employee.FirstOrDefault(e => e.Id == id);

        public List<Employee> GetAll()
            => _orderingContext.Employee.ToList();
    }
}
