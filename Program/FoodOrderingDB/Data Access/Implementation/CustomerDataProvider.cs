using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation
{
    class CustomerDataProvider : IDataProvider<Customer>
    {
        OrderingContext context = new OrderingContext();
        Customer _entity;
        public CustomerDataProvider(Customer entity)
        {
            _entity = entity;
        }
        public DbSet<Customer> GetContext()
        {
            return context.Customer;
        }
        public Customer GetEntity()
        {
            return _entity;
        }
        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
