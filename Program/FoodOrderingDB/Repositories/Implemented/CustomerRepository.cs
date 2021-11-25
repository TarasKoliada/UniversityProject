using FoodOrderingDB.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Repositories.Implemented
{
    class CustomerRepository : IRepository<Customer>
    {
        private readonly OrderingContext _orderingContext;
        public CustomerRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Customer entity)
        {
            if (entity != null)
            {
                _orderingContext.Customer.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var customer = Get(id);

            if (customer != null)
            {
                _orderingContext.Customer.Remove(customer);
                _orderingContext.SaveChanges();
            }
        }
        public void SaveData()
        {
            _orderingContext.SaveChanges();
        }

        public Customer Get(int id)
            => _orderingContext.Customer.FirstOrDefault(c => c.Id == id);

        public List<Customer> GetAll()
            => _orderingContext.Customer.ToList();
    }
}
